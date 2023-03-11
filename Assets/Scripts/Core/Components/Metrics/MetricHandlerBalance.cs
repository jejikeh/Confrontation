using System.Collections.Generic;
using Wooff.ECS.Components;

namespace Core.Components.Metrics
{
    public class MetricHandlerBalance : IComponent
    {
        public Dictionary<MetricType, float> Balance = new Dictionary<MetricType, float>()
        {
            { MetricType.Gold, 0 },
            { MetricType.SpeedCreationUnits, 0 }
        };

        public void AddToMetric(MetricType metricType, float amount)
        {
            Balance[metricType] += amount;
        }
    }
}