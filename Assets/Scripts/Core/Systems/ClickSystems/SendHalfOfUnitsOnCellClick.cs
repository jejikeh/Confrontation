using Core.Components;
using Core.Components.Metrics;
using Core.Components.Tags;
using JetBrains.Annotations;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

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

            SendUnitsToOtherProperty(_firstClickedEntity, clickedEntity, _playerQueue);
            _firstClickedEntity = null;
        }

        public static bool SendUnitsToOtherProperty(IEntity fromEntityCell, IEntity toEntityCell, PlayerQueue playerQueue)
        {
            if (!fromEntityCell.ContextContains<PropertyComponent>() ||
                !toEntityCell.ContextContains<PropertyComponent>())
                return false;

            if (!fromEntityCell.ContextContains<MetricHandlerBalanceComponent>() ||
                !toEntityCell.ContextContains<MetricHandlerBalanceComponent>())
                return false;

            if (fromEntityCell.ContextGet<PropertyComponent>().Owner != playerQueue.CurrentTurnPlayer())
                return false;
            
            if (fromEntityCell.ContextGet<PropertyComponent>()?.Owner ==
                toEntityCell.ContextGet<PropertyComponent>()?.Owner)
            {
                var fromEntityCellBalanceHandler = fromEntityCell.ContextGet<MetricHandlerBalanceComponent>();

                int unitSendCount;
                if (fromEntityCellBalanceHandler.Balance[MetricType.Units] == 2)
                    unitSendCount = 2;
                else if ((fromEntityCellBalanceHandler.Balance[MetricType.Units] / 2) % 2 != 0)
                    unitSendCount = (int)((fromEntityCellBalanceHandler.Balance[MetricType.Units] + 1) / 2);
                else
                    unitSendCount = (int)fromEntityCellBalanceHandler.Balance[MetricType.Units] / 2;

                fromEntityCellBalanceHandler.RemoveFromMetric(MetricType.Units, unitSendCount);
                toEntityCell.ContextGet<MetricHandlerBalanceComponent>()?.AddToMetric(MetricType.Units, unitSendCount);
                toEntityCell.ContextGet<PropertyComponent>()?
                    .Owner.ContextGet<MetricHandlerBalanceComponent>()?
                    .RemoveFromMetric(MetricType.Move, MovePrice.SendUnitsPrice);
            }

            return true;
        }
    }
}