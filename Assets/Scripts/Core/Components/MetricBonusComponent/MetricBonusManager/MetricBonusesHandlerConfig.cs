using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components.MetricComponent;
using UnityEngine;
using Wooff.ECS;

namespace Core.Components.MetricBonusComponent.MetricBonusManager
{
    [Serializable]
    public class MetricBonusesHandlerConfig : IConfig
    {
        public List<MetricBonusConfig> MetricBonusConfigs = new List<MetricBonusConfig>();
        
        public MetricBonusConfig GetMetricBonusConfig(MetricType metricType)
        {
            return MetricBonusConfigs.FirstOrDefault(x => x.MetricType == metricType);
        }
    }
}