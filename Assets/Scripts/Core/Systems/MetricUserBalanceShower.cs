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
        private List<PlayerTagComponent> _playerTagComponents = new List<PlayerTagComponent>();

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _metricShowerWindowComponent = context
                .ContextGetAllFromMap(typeof(MetricShowerWindowComponent))
                .FirstOrDefault()
                .ContextGet<MetricShowerWindowComponent>();

            _playerTagComponents = context
                .ContextGetAllFromMap(typeof(PlayerComponent)).Select(x => x.ContextGet<PlayerTagComponent>()).ToList();
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            foreach (var playerTagComponent in _playerTagComponents.Where(playerTagComponent => playerTagComponent.PlayerComponent.Turn))
                _metricShowerWindowComponent.UpdatePlayerInformation(playerTagComponent.MetricHandlerBalanceComponent, playerTagComponent.PlayerComponent.Color);
        }
    }
}