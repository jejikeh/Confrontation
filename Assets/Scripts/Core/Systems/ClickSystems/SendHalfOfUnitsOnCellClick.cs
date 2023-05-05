using System;
using System.Linq;
using Core.Components;
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
                
            }

            _firstClickedEntity = null;
        }

        /// <summary>
        /// Sending units to other settlements
        /// </summary>
        /// <param name="fromEntityCell">Where units will be sent from</param>
        /// <param name="toEntityCell">Where will they go</param>
        /// <param name="playerQueue">Auxiliary link to the queue system</param>
        /// <returns></returns>
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
            if (fromEntityCell.ContextGet<PropertyComponent>()?.Owner ==
                toEntityCell.ContextGet<PropertyComponent>()?.Owner)
                return (CalculateUnitCount(fromEntityCell, playerQueue.CurrentTurnPlayer().ContextGet<MetricHandlerBalanceComponent>()), SendUnitAction.SendToProperty);

            return (0, SendUnitAction.None);
        }

        /// <summary>
        /// Send units cell to which the units are sent is an allied one
        /// </summary>
        /// <param name="fromEntityCell"></param>
        /// <param name="toEntityCell"></param>
        private static void HandleMetricWhenSendUnitsToProperty(IEntity fromEntityCell, IEntity toEntityCell, int unitSendCount)
        {
            var fromEntityCellBalanceHandler = fromEntityCell.ContextGet<MetricHandlerBalanceComponent>();
            fromEntityCellBalanceHandler.RemoveFromMetric(MetricType.Units | MetricType.Protection, unitSendCount);
            
            toEntityCell.ContextGet<MetricHandlerBalanceComponent>()?.AddToMetric(MetricType.Units | MetricType.Protection, unitSendCount);
            toEntityCell.ContextGet<PropertyComponent>()?.Owner.ContextGet<MetricHandlerBalanceComponent>()?.RemoveFromMetric(MetricType.Move, MovePrice.SendUnitsPrice);
        }

        private static int CalculateUnitCount(IEntity fromEntityCell, MetricHandlerBalanceComponent playerBalance)
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

        public enum SendUnitAction
        {
            None,
            SendToProperty,
            SendToEnemy
        }
    }
}