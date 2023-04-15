using System.Collections.Generic;
using Wooff.ECS.Components;

namespace Core.Components.Metrics
{
    public class MetricHandlerBalanceComponent : IComponent
    {
        public Dictionary<MetricType, float> Balance { get; set; }

        public void AddToMetric(MetricType metricType, float amount)
        {
            Balance[metricType] += amount;
        }

        public void SetMetric(MetricType metricType, float value)
        {
            Balance[metricType] = value;
        }
        
        public void RemoveFromMetric(MetricType metricType, float amount)
        {
            Balance[metricType] -= amount;
        }
    }
}