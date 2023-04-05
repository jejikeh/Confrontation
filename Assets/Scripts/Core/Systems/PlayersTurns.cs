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
        private Queue<IEntity> _cachedPlayers = new Queue<IEntity>();
        private List<IEntity> _cachedCells = new List<IEntity>();

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var players = context
                .ContextGetAllFromMap(typeof(PlayerComponent));

            foreach (var player in players)
                _cachedPlayers.Enqueue(player);
        }
        
        public override async void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCells.Count != context.Count<CellTagComponent>())
            {
                var newEntities = context
                    .ContextWhereQuery(x => x.ContextContains<CellTagComponent>())
                    .Where(x => !_cachedCells.Contains(x))
                    .ToArray();
                
                if(!newEntities.Any())
                    return;
                
                _cachedCells.AddRange(newEntities);
            }
            
            var player = _cachedPlayers.Peek().ContextGet<PlayerComponent>();
            if (GameStateManager.GetTurnState == TurnState.StartTurn)
            {
                player.Turn = true;
                // todo: move this to metrics miner
                _cachedPlayers.Peek().ContextGet<MetricHandlerBalanceComponent>().AddToMetric(MetricType.Move, 2);
                GameStateManager.SetTurnState(TurnState.ProcessTurn);
            }
            else if (GameStateManager.GetTurnState == TurnState.EndTurn)
            {
                player.Turn = false;
                _cachedPlayers.Enqueue(_cachedPlayers.Dequeue());
                GameStateManager.SetTurnState(TurnState.StartTurn);
            }

            if (ChooseCellWindowMonoReference.GetState != CellType.None && player.PlayerType == PlayerType.User)
            {
                var chooseCellWindowComponent = context
                    .ContextWhereQuery(x => x.ContextContains<ChooseCellWindowComponent>())
                    .FirstOrDefault()
                    .ContextGet<ChooseCellWindowComponent>();
                
                ReplaceCell(
                    _cachedPlayers.Peek(),
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

            var playerBalance = player.ContextGet<MetricHandlerBalanceComponent>();
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
                    .Where(p => p.ContextGet<PropertyComponent>().Owner == player)
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
            
            player.ContextGet<MetricHandlerBalanceComponent>().RemoveFromMetric(MetricType.Gold, 1);
            player.ContextGet<MetricHandlerBalanceComponent>().RemoveFromMetric(MetricType.Move, 1);

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