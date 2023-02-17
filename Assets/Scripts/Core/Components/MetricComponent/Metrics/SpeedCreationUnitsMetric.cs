using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.MetricComponent.Metrics
{
    public class SpeedCreationUnitsMetric : Metric
    {
        public SpeedCreationUnitsMetric(MetricConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}