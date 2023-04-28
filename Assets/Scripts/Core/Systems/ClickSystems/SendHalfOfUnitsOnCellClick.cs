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
        [CanBeNull] private IEntity _lastPressedEntity;
        
        protected override void ProcessClickedEntity(EntityContext context, IEntity clickedEntity)
        {
            if (GameStateManager.GetUiState != UiState.SendUnits)
            {
                _lastPressedEntity = null;
                return;
            }

            if (_lastPressedEntity == null)
            {
                _lastPressedEntity = clickedEntity;
                return;
            }
            
            if (!SendUnitsToOtherProperty(_lastPressedEntity, clickedEntity)) 
                return;

            _lastPressedEntity = null;
        }

        public static bool SendUnitsToOtherProperty(IEntity lastPressedEntity, IEntity clickedEntity)
        {
            if (!lastPressedEntity.ContextContains<PropertyComponent>() ||
                !clickedEntity.ContextContains<PropertyComponent>())
                return false;

            if (!lastPressedEntity.ContextContains<MetricHandlerBalanceComponent>() ||
                !clickedEntity.ContextContains<MetricHandlerBalanceComponent>())
                return false;

            if (lastPressedEntity.ContextGet<PropertyComponent>()?.Owner ==
                clickedEntity.ContextGet<PropertyComponent>()?.Owner)
            {
                var lastPressedBalanceHandler = lastPressedEntity.ContextGet<MetricHandlerBalanceComponent>();

                int unitCount;
                if (lastPressedBalanceHandler.Balance[MetricType.Units] == 2)
                    unitCount = 2;
                else if ((lastPressedBalanceHandler.Balance[MetricType.Units] / 2) % 2 != 0)
                    unitCount = (int)((lastPressedBalanceHandler.Balance[MetricType.Units] + 1) / 2);
                else
                    unitCount = (int)lastPressedBalanceHandler.Balance[MetricType.Units] / 2;

                lastPressedBalanceHandler.RemoveFromMetric(MetricType.Units, unitCount);
                clickedEntity.ContextGet<MetricHandlerBalanceComponent>()?.AddToMetric(MetricType.Units, unitCount);
            }

            return true;
        }
    }
}