using System.Collections.Generic;
using Wooff.ECS.Components;

namespace Core.Components.Metrics
{
    public class MetricHandlerBalanceComponent : IComponent
    {
        public Dictionary<MetricType, float> Balance = new Dictionary<MetricType, float>()
        {
            { MetricType.Move, 10 },
            { MetricType.Gold, 2 },
            { MetricType.SpeedCreationUnits, 2 }
        };

        public void AddToMetric(MetricType metricType, float amount)
        {
            Balance[metricType] += amount;
        }
        
        public void RemoveFromMetric(MetricType metricType, float amount)
        {
            Balance[metricType] -= amount;
        }
    }
}