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

        public static bool SendUnitsToOtherProperty(IEntity firstClickedEntity, IEntity secondClickedEntity, PlayerQueue playerQueue)
        {
            if (!firstClickedEntity.ContextContains<PropertyComponent>() ||
                !secondClickedEntity.ContextContains<PropertyComponent>())
                return false;

            if (!firstClickedEntity.ContextContains<MetricHandlerBalanceComponent>() ||
                !secondClickedEntity.ContextContains<MetricHandlerBalanceComponent>())
                return false;

            if (firstClickedEntity.ContextGet<PropertyComponent>().Owner != playerQueue.CurrentTurnPlayer())
                return false;
            
            if (firstClickedEntity.ContextGet<PropertyComponent>()?.Owner ==
                secondClickedEntity.ContextGet<PropertyComponent>()?.Owner)
            {
                var lastPressedBalanceHandler = firstClickedEntity.ContextGet<MetricHandlerBalanceComponent>();

                int unitCount;
                if (lastPressedBalanceHandler.Balance[MetricType.Units] == 2)
                    unitCount = 2;
                else if ((lastPressedBalanceHandler.Balance[MetricType.Units] / 2) % 2 != 0)
                    unitCount = (int)((lastPressedBalanceHandler.Balance[MetricType.Units] + 1) / 2);
                else
                    unitCount = (int)lastPressedBalanceHandler.Balance[MetricType.Units] / 2;

                lastPressedBalanceHandler.RemoveFromMetric(MetricType.Units, unitCount);
                secondClickedEntity.ContextGet<MetricHandlerBalanceComponent>()?.AddToMetric(MetricType.Units, unitCount);
            }

            return true;
        }
    }
}