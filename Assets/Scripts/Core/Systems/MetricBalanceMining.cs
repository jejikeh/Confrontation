using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.Metrics;
using JetBrains.Annotations;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class MetricBalanceMining : Wooff.ECS.Systems.System
    {
        // TODO: Bad decision. Fix it
        [CanBeNull] 
        public static IEntity CurrentTurnEntity { get; set; }
        
        private List<IEntity> _cachedEntities = new List<IEntity>();
        private int _cachedCount;

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if(CurrentTurnEntity is null)
                return;
            
            if (_cachedCount != context.Count<PropertyComponent>())
            {
                _cachedEntities = context
                    .ContextWhereQuery(x => x.ContextContains<PropertyComponent>())
                    .Where(x => !_cachedEntities.Contains(x))
                    .ToList();
                
                _cachedCount = context.Count<PropertyComponent>();
            }
            
            foreach (var entity in _cachedEntities)
            {
                if(!entity.ContextContains<MetricsMinerComponent>())
                    continue;
                var metricsMiner = entity.ContextGet<MetricsMinerComponent>();
                
                var property = entity.ContextGet<PropertyComponent>();
                
                // TODO: Bad decision. Fix it
                if(property.Owner != CurrentTurnEntity)
                    continue;
                
                if(!property.Owner.ContextContains<MetricHandlerBalanceComponent>())
                    continue;
                
                var ownerBalance = property.Owner.ContextGet<MetricHandlerBalanceComponent>();

                foreach (var mine in metricsMiner.Metrics)
                    ownerBalance.AddToMetric(mine, metricsMiner.BonusAmount);
            }

            CurrentTurnEntity = null;
        }
    }
}