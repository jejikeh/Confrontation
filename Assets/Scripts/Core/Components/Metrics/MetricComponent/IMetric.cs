using Wooff.ECS.Contexts;

namespace Core.Components.Metrics.MetricComponent
{
    public interface IMetric : IContextItem
    {
        public int Amount { get; }
        public void AddToMetric(int amount);
        public MetricType MetricType { get; }
    }
}