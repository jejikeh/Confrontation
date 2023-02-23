using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components.Metrics.MetricComponent;
using Wooff.ECS;

namespace Core.Components.Metrics.MetricMinerComponent.MetricMinerManager
{
    [Serializable]
    public class MetricMinerHandlerConfig : IConfig
    {
        public List<MetricMinerConfig> MetricBonusConfigs = new List<MetricMinerConfig>();
        public MetricMinerConfig GetMetricBonusConfig(MetricType metricType)
        {
            return MetricBonusConfigs.FirstOrDefault(x => x.MetricType == metricType);
        }
    }
}