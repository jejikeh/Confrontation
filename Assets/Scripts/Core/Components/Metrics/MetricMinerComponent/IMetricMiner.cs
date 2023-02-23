using Core.Components.Metrics.MetricComponent;
using Wooff.ECS.Contexts;

namespace Core.Components.Metrics.MetricMinerComponent
{
    public interface IMetricMiner : IContextItem
    {
        public int GetBonusAmount();
        public MetricType MetricType { get; }
    }
}