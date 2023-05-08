using System;
using System.Linq;
using Core.Components;
using Core.Components.CellRelated;
using Core.Components.Metrics;
using Core.Components.Tags;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using JetBrains.Annotations;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Random = UnityEngine.Random;

namespace Core.Systems.ClickSystems
{
    public class SendHalfOfUnitsOnCellClick : HandleClickedState<CellTagComponent>
    {
        [CanBeNull] private IEntity _firstClickedEntity;
        private PlayerQueue _playerQueue;
        
        public SendHalfOfUnitsOnCellClick(PlayerQueue playerQueue)
        {
            _playerQueue = playerQueue;
        }
        
        protected override void ProcessClickedEntity(EntityContext context, IEntity clickedEntity)
        {
            if (GameStateManager.GetUiState != UiState.SendUnits)
            {
                _firstClickedEntity = null;
                return;
            }

            if (_firstClickedEntity == null)
            {
                _firstClickedEntity = clickedEntity;
                return;
            }
            
            var actionDetector = SendUnitsToOtherProperty(_firstClickedEntity, clickedEntity, _playerQueue);
            
            var unityObjectOfFirstClickedEntity = _firstClickedEntity.ContextGet<UnityGameObjectComponent>();
            var positionOfClickedEntity = unityObjectOfFirstClickedEntity.StartPosition;
            var rotationOfClickedEntity = unityObjectOfFirstClickedEntity.StartRotation;
            if (actionDetector.Item2 == SendUnitAction.SendToProperty)
            {
                foreach (var _ in Enumerable.Range(0, actionDetector.Item1))
                {
                    context.ContextAdd(new UnitTagComponent(
                        UnitPrefabsHandler.RandomUnitData(),
                        positionOfClickedEntity,
                        rotationOfClickedEntity)
                        .CreateTagEntityContainerMovingFromAtoB(_firstClickedEntity, clickedEntity, HandleMetricWhenSendUnitsToProperty));
                }
            } 
            else if (actionDetector.Item2 == SendUnitAction.SendToEnemy)
            {
                foreach (var _ in Enumerable.Range(0, actionDetector.Item1))
                {
                    context.ContextAdd(new UnitTagComponent(
                            UnitPrefabsHandler.RandomUnitData(),
                            positionOfClickedEntity,
                            rotationOfClickedEntity)
                        .CreateTagEntityContainerMovingFromAtoB(_firstClickedEntity, clickedEntity, HandleMetricWhenSendUnitsToEnemy));
                }
            }

            _firstClickedEntity = null;
        }

        public static (int, SendUnitAction) SendUnitsToOtherProperty(IEntity fromEntityCell, IEntity toEntityCell, PlayerQueue playerQueue)
        {
            if (!fromEntityCell.ContextContains<PropertyComponent>() ||
                !toEntityCell.ContextContains<PropertyComponent>())
                return (0, SendUnitAction.None);

            if (!fromEntityCell.ContextContains<MetricHandlerBalanceComponent>() ||
                !toEntityCell.ContextContains<MetricHandlerBalanceComponent>())
                return (0, SendUnitAction.None);
            
            if (fromEntityCell.ContextGet<PropertyComponent>().Owner != playerQueue.CurrentTurnPlayer())
                return (0, SendUnitAction.None);

            // If the cell to which the units are sent is an allied one
            if (fromEntityCell.ContextGet<PropertyComponent>()?.Owner == toEntityCell.ContextGet<PropertyComponent>()?.Owner)
                return (CalculateUnitCountWhenSendToProperty(fromEntityCell, playerQueue.CurrentTurnPlayer().ContextGet<MetricHandlerBalanceComponent>()), SendUnitAction.SendToProperty);
            else
                return (CalculateUnitCountWhenSendToEnemy(fromEntityCell, playerQueue.CurrentTurnPlayer().ContextGet<MetricHandlerBalanceComponent>()), SendUnitAction.SendToEnemy);
        }

        private static void HandleMetricWhenSendUnitsToProperty(IEntity fromEntityCell, IEntity toEntityCell, int unitSendCount, EntityContext _)
        {
            var fromEntityCellBalanceHandler = fromEntityCell.ContextGet<MetricHandlerBalanceComponent>();
            fromEntityCellBalanceHandler.RemoveFromMetric(fromEntityCellBalanceHandler.GetMetricHandledFlags(), unitSendCount);
            
            toEntityCell.ContextGet<MetricHandlerBalanceComponent>()?.AddToMetric(fromEntityCellBalanceHandler.GetMetricHandledFlags(), unitSendCount);
            toEntityCell.ContextGet<PropertyComponent>()?.Owner.ContextGet<MetricHandlerBalanceComponent>()?.RemoveFromMetric(MetricType.Move, MovePrice.SendUnitsPrice);
        }
        
        private static void HandleMetricWhenSendUnitsToEnemy(IEntity fromEntityCell, IEntity toEntityCell, int unitSendCount, EntityContext context)
        {
            var toEntityCellBalanceHandler = toEntityCell.ContextGet<MetricHandlerBalanceComponent>();
            if (toEntityCellBalanceHandler?.Balance[MetricType.Units] == 0)
                return;

            var fromEntityCellBalanceHandler = fromEntityCell.ContextGet<MetricHandlerBalanceComponent>();
            fromEntityCellBalanceHandler?.RemoveFromMetric(fromEntityCellBalanceHandler.GetMetricHandledFlags(), unitSendCount);
            
            if (toEntityCellBalanceHandler?.Balance[MetricType.Protection] >= unitSendCount)
                toEntityCellBalanceHandler.RemoveFromMetric(MetricType.Protection, unitSendCount);
            else
            {
                var attackAfterDefence = unitSendCount - toEntityCellBalanceHandler.Balance[MetricType.Protection];
                toEntityCellBalanceHandler?.RemoveFromMetric(MetricType.Protection, toEntityCellBalanceHandler.Balance[MetricType.Protection]);
                toEntityCellBalanceHandler?.RemoveFromMetric(MetricType.Units, attackAfterDefence);
            }

            if (toEntityCellBalanceHandler?.Balance[MetricType.Units] == 0)
                PlayerTurn.ReplaceCell(
                    fromEntityCell.ContextGet<PropertyComponent>()?.Owner,
                    toEntityCell,
                    toEntityCell.ContextGet<CellComponent>().CellType,
                    context);
            
            fromEntityCellBalanceHandler.AddToMetric(MetricType.Units, Random.Range(1, unitSendCount));
            fromEntityCell.ContextGet<PropertyComponent>()?.Owner.ContextGet<MetricHandlerBalanceComponent>()?.RemoveFromMetric(MetricType.Move, MovePrice.SendUnitsPrice);
        }
        
        private static int CalculateUnitCountWhenSendToProperty(IEntity fromEntityCell, MetricHandlerBalanceComponent playerBalance)
        {
            var fromEntityCellBalanceHandler = fromEntityCell.ContextGet<MetricHandlerBalanceComponent>();
            
            var unitSendCount = 0;
            if (fromEntityCellBalanceHandler.Balance[MetricType.Units] == 2)
                unitSendCount = 2;
            else if ((fromEntityCellBalanceHandler.Balance[MetricType.Units] / 2) % 2 != 0)
                unitSendCount = (int)((fromEntityCellBalanceHandler.Balance[MetricType.Units] + 1) / 2);
            else
                unitSendCount = (int)fromEntityCellBalanceHandler.Balance[MetricType.Units] / 2;
            
            return Math.Clamp(unitSendCount, 2, (int)playerBalance.Balance[MetricType.Move]);
        }
        
        private static int CalculateUnitCountWhenSendToEnemy(IEntity fromEntityCell, MetricHandlerBalanceComponent playerBalance)
        {
            var fromEntityCellBalanceHandler = fromEntityCell.ContextGet<MetricHandlerBalanceComponent>();
            
            var unitSendCount = fromEntityCellBalanceHandler.Balance[MetricType.Attack];

            if ((int)unitSendCount == 1 && playerBalance.Balance[MetricType.Move] > 0 )
                return 1;
                
            return (int)Math.Clamp(unitSendCount, 2, playerBalance.Balance[MetricType.Move]);
        }

        public enum SendUnitAction
        {
            None,
            SendToProperty,
            SendToEnemy
        }
    }
}