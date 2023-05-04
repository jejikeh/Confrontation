using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.Metrics;
using Core.Components.Players;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class MetricBalanceMining : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedProperties = new List<IEntity>();
        private List<IEntity> _cachedPlayers = new List<IEntity>();
        private IEntity _cachedTurnPlayer;

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _cachedPlayers = context.ContextGetAllFromMap(typeof(PlayerComponent)).ToList();
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            var turnPlayer = _cachedPlayers.FirstOrDefault(x => x.ContextGet<PlayerComponent>().Turn);
            
            if (_cachedTurnPlayer == turnPlayer)
                return;

            _cachedTurnPlayer = turnPlayer;
            
            if (_cachedProperties.Count != context.Count<PropertyComponent>())
                _cachedProperties = context
                    .ContextWhereQuery(x => x.ContextContains<PropertyComponent>())
                    .ToList();
            
            foreach (var entity in _cachedProperties)
            {
                if(!entity.ContextContains<MetricsMinerComponent>())
                    continue;
                
                var metricsMiner = entity.ContextGet<MetricsMinerComponent>();
                var property = entity.ContextGet<PropertyComponent>();
                if (property.Owner != _cachedTurnPlayer)
                    continue;
                
                if (!property.Owner.ContextContains<MetricHandlerBalanceComponent>())
                    continue;
                
                var ownerBalance = property.Owner.ContextGet<MetricHandlerBalanceComponent>();
                const MetricType cellMetrics = MetricType.Protection | MetricType.Attack | MetricType.Units;
                foreach (var metric in metricsMiner.Metrics)
                {
                    if ((metric & cellMetrics) != MetricType.None)
                        entity.ContextGet<MetricHandlerBalanceComponent>()?.AddToMetric(metric, metricsMiner.BonusAmount);
                    
                    ownerBalance?.AddToMetric(metric, metricsMiner.BonusAmount);
                }
            }
        }
    }
}