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
    public class PlayerTurn : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedPlayers = new List<IEntity>();
        private readonly List<IEntity> _cachedCells = new List<IEntity>();

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _cachedPlayers = context
                .ContextGetAllFromMap(typeof(PlayerComponent)).ToList();
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCells.Count != context.Count<CellTagComponent>())
            {
                var newEntities = context
                    .ContextWhereQuery(x => x.ContextContains<CellTagComponent>())
                    .Where(x => !_cachedCells.Contains(x))
                    .ToArray();

                if (!newEntities.Any())
                    return;

                _cachedCells.AddRange(newEntities);
            }

            var playerUser = _cachedPlayers.FirstOrDefault(x => x.ContextGet<PlayerComponent>().Turn && x.ContextGet<PlayerComponent>().PlayerType == PlayerType.User);
            
            if (ChooseCellWindowMonoReference.GetState == CellType.None)
                return;
            
            var chooseCellWindowComponent = context
                .ContextWhereQuery(x => x.ContextContains<ChooseCellWindowComponent>())
                .FirstOrDefault()
                .ContextGet<ChooseCellWindowComponent>();
            
            if (chooseCellWindowComponent == null) 
                return;
            
            ReplaceCell(
                playerUser,
                chooseCellWindowComponent.ClickedEntity,
                ChooseCellWindowMonoReference.GetState,
                context);

            chooseCellWindowComponent.UpdateClickedCell(null);
            ChooseCellWindowMonoReference.StateHandled();
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

            player.ContextGet<MetricHandlerBalanceComponent>()?.RemoveFromMetric(MetricType.Gold, 1);
            player.ContextGet<MetricHandlerBalanceComponent>()?.RemoveFromMetric(MetricType.Move, 1);

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
