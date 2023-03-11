using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.UiRelated.Windows.MetricShower;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class MetricBalanceMining : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedEntities = new List<IEntity>();
        // TODO: cache not the entities count but count from map component|list entity 
        private int _cachedCount;
        
        private const float Time = 3f;
        private float _timer;
        private MetricShowerWindowComponent _metricShowerWindowComponent;
        
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _metricShowerWindowComponent =
                context.ContextGetAllFromMap(typeof(MetricShowerWindowComponent)).FirstOrDefault().ContextGet<MetricShowerWindowComponent>();
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_timer < Time)
            {
                _timer += UnityEngine.Time.deltaTime * timeScale;
                return;
            }

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
                if(!property.Owner.ContextContains<MetricHandlerBalance>())
                    continue;
                
                var ownerBalance = property.Owner.ContextGet<MetricHandlerBalance>();

                foreach (var mine in metricsMiner.Mines)
                {
                    ownerBalance.AddToMetric(mine, metricsMiner.BonusAmount);
                    Debug.Log($"Added to metric {mine} bonus - {metricsMiner.BonusAmount}");
                }
                
                if(property.Owner.ContextGet<PlayerComponent>().PlayerType == PlayerType.User)
                    _metricShowerWindowComponent.UpdateMetrics(ownerBalance);
            }

            _timer = 0;
        }
    }
}