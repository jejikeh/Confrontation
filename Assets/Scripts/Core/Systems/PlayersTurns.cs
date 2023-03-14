using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.CellRelated;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.Tags;
using Core.Components.UiRelated.Windows.ChooseCell;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class PlayersTurns : Wooff.ECS.Systems.System
    {
        private Queue<IEntity> _cachedEntities = new Queue<IEntity>();
        private bool _isSetToTurn;

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var players = context
                .ContextGetAllFromMap(typeof(PlayerComponent));

            foreach (var player in players)
                _cachedEntities.Enqueue(player);
        }
        
        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            var player = _cachedEntities.Peek().ContextGet<PlayerComponent>();
            if (GameStateManager.GetTurnState == TurnState.StartTurn && !_isSetToTurn)
            {
                player.Turn = true;
                _isSetToTurn = true;
                _cachedEntities.Peek().ContextGet<MetricHandlerBalance>().AddToMetric(MetricType.Move, 2);
                MetricBalanceMining.CurrentTurnEntity = _cachedEntities.Peek();
            }

            if (GameStateManager.GetTurnState == TurnState.EndTurn && _isSetToTurn)
            {
                player.Turn = false;
                _isSetToTurn = false;
                _cachedEntities.Enqueue(_cachedEntities.Dequeue());
                GameStateManager.SetTurnState(TurnState.StartTurn);
            }

            if (player.PlayerType == PlayerType.User && ChooseCellWindowMonoReference.GetState != CellType.None)
            {
                var chooseCellWindowComponent = context
                    .ContextWhereQuery(x => x.ContextContains<ChooseCellWindowComponent>())
                    .FirstOrDefault()
                    .ContextGet<ChooseCellWindowComponent>();
                
                ReplaceCell(
                    _cachedEntities.Peek(),
                    chooseCellWindowComponent.ClickedEntity,
                    ChooseCellWindowMonoReference.GetState, 
                    context);
                
                chooseCellWindowComponent.UpdateClickedCell(null);
                ChooseCellWindowMonoReference.StateHandled();
            }
        }
        
        private void ReplaceCell(IEntity player, IEntity clickedEntity, CellType cellType, EntityContext entityContext)
        {
            if (clickedEntity is null || !clickedEntity.ContextGet<CellComponent>().Plain)
                return;

            var playerBalance = player.ContextGet<MetricHandlerBalance>();
            if (playerBalance.Balance[MetricType.Gold] <= 0)
                return;

            if (playerBalance.Balance[MetricType.Move] <= 0)
                return;

            if (!player.ContextGet<PlayerComponent>().Turn)
                return;
            
            if (entityContext.Count<PropertyComponent>() > 0)
            {
                var allPropertyCells = entityContext.ContextWhereQuery(x =>
                        x.ContextContains<PropertyComponent>())
                    .Where(p => p.ContextGet<PropertyComponent>().Owner.ContextGet<PlayerComponent>().PlayerType == PlayerType.User)
                    .Select(x => x.ContextGet<UnityGameObjectComponent>());

                if (allPropertyCells.Any())
                {
                    var cellPosition = clickedEntity
                        .ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform.position;
                    if (!allPropertyCells.Any(x =>
                            Vector3.Distance(
                                cellPosition,
                                x.UnitySceneObject.transform.position) <
                            player.ContextGet<PlayerComponent>().MaxBuildDistance))
                        return;
                }
            }
            
            player.ContextGet<MetricHandlerBalance>().RemoveFromMetric(MetricType.Gold, 1);
            player.ContextGet<MetricHandlerBalance>().RemoveFromMetric(MetricType.Move, 1);

            var unityObject = clickedEntity
                .ContextGet<UnityGameObjectComponent>();
            var startPosition = unityObject.StartPosition;
            var startRotation = unityObject.StartRotation;

            entityContext.ContextAdd(
                new CellTagComponent(
                        CellPrefabsHandler.GetCellComponent(cellType), 
                        startPosition, 
                        startRotation)
                    .CreateCellEntityContainerAsProperty(player));

            clickedEntity.ContextGet<HealthComponent>().Kill();
        }
    }
}