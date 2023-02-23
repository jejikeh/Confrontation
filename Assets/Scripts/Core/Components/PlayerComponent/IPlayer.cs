using Core.Components.Metrics.MetricComponent.MetricManager;
using Wooff.ECS.Contexts;

namespace Core.Components.PlayerComponent
{
    public interface IPlayer : IContextItem
    {
        public PlayerType PlayerType { get; }
        public MetricHandler MetricHandler { get; }
    }
}