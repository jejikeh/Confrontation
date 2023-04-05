using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.Metrics;
using Core.Components.Players;
using JetBrains.Annotations;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class MetricBalanceMining : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedProperties = new List<IEntity>();
        
        private IEntity _turnPlayer;

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if(GameStateManager.GetTurnState == TurnState.ProcessTurn)
                return;
            
            if (_cachedProperties.Count != context.Count<PropertyComponent>())
                _cachedProperties = context
                    .ContextWhereQuery(x => x.ContextContains<PropertyComponent>())
                    .ToList();

            _turnPlayer = context
                .ContextWhereQuery(x => x.ContextContains<PlayerComponent>())
                .FirstOrDefault(x => x.ContextGet<PlayerComponent>().Turn);
            
            foreach (var entity in _cachedProperties)
            {
                if(!entity.ContextContains<MetricsMinerComponent>())
                    continue;
                var metricsMiner = entity.ContextGet<MetricsMinerComponent>();
                
                var property = entity.ContextGet<PropertyComponent>();
                
                if(property.Owner != _turnPlayer)
                    continue;
                
                if(!property.Owner.ContextContains<MetricHandlerBalanceComponent>())
                    continue;
                
                var ownerBalance = property.Owner.ContextGet<MetricHandlerBalanceComponent>();

                foreach (var mine in metricsMiner.Metrics)
                    ownerBalance.AddToMetric(mine, metricsMiner.BonusAmount);
            }
        }
    }
}