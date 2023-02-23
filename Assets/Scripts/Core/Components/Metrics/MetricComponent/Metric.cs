using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.Metrics.MetricComponent
{
    public class Metric : IMetric
    {
        public MetricType MetricType { get; private set; }
        public int Amount { get; private set; }

        public Metric(MetricConfig data)
        {
            MetricType = data.MetricType;
            Amount = data.StartAmount;
        }

        public void AddToMetric(int amount)
        {
            Debug.Log($"Added to {MetricType} metric {amount}");
            Amount += amount;
        }
    }
}