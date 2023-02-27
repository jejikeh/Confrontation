using System;
using Core.Components.Metrics.MetricComponent;
using Wooff.ECS;

namespace Core.Components.Metrics.MetricMinerComponent
{
    [Serializable]
    public class MetricMinerConfig : IConfig
    {
        public int BonusAmount;
        public MetricType MetricType;
        public bool MineEverySecond = true;
    }
}