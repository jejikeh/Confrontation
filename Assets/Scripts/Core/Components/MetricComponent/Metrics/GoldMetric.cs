using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.MetricComponent.Metrics
{
    public class GoldMetric : Metric
    {
        public GoldMetric(MetricConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}