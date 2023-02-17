using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.Metrics
{
    public class Metric : Component<MetricConfig, IMonoEntity>
    {
        public int Amount { get; private set; }

        public Metric(MetricConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        public void AddToMetric(int amount)
        {
            Amount += amount;
        }
    }
}