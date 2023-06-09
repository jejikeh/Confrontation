﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Wooff.ECS.Components;

namespace Core.Components.Metrics
{
    public class MetricHandlerBalanceComponent : IComponent
    {
        public Dictionary<MetricType, float> Balance { get; set; }

        public void AddToMetric(MetricType metricType, float amount)
        {
            foreach (var value in Enum.GetValues(metricType.GetType()))
            {
                if (metricType.HasFlag((MetricType)value) && (MetricType)value != MetricType.None &&
                    Balance.ContainsKey((MetricType)value))
                    Balance[(MetricType)value] += amount;
            }
        }

        public void SetMetric(MetricType metricType, float value)
        {
            Balance[metricType] = value;
        }
        
        public void RemoveFromMetric(MetricType metricType, float amount)
        {
            foreach (var value in Enum.GetValues(metricType.GetType()))
            {
                if (metricType.HasFlag((MetricType)value) && (MetricType)value != MetricType.None &&
                    Balance.ContainsKey((MetricType)value))
                {
                    Balance[(MetricType)value] -= amount;
                    if (Balance[(MetricType)value] <= 0)
                        Balance[(MetricType)value] = 0;
                }
            }
        }

        public MetricType GetMetricHandledFlags()
        {
            var metricHandledFlags = MetricType.None;
            foreach (var metric in Balance)
                if(metric.Value > 0 )
                    metricHandledFlags |= metric.Key;

            return metricHandledFlags;
        }
    }
}