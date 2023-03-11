using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.Tags;
using Core.Components.UiRelated.Windows.MetricShower;
using JetBrains.Annotations;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class MetricUserBalanceShower : Wooff.ECS.Systems.System
    {
        private MetricShowerWindowComponent _metricShowerWindowComponent;
        private PlayerTagComponent _playerTagComponent;

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _metricShowerWindowComponent = context
                .ContextGetAllFromMap(typeof(MetricShowerWindowComponent))
                .FirstOrDefault()
                .ContextGet<MetricShowerWindowComponent>();
            
            _playerTagComponent = context
                .ContextGetAllFromMap(typeof(PlayerTagComponent))
                .FirstOrDefault(x => x.ContextGet<PlayerComponent>().PlayerType == PlayerType.User)
                .ContextGet<PlayerTagComponent>();
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            _metricShowerWindowComponent.UpdateMetrics(_playerTagComponent.MetricMinerComponent);
        }
    }
}