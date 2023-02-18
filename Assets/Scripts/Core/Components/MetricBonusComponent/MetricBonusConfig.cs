using System;
using Core.Components.MetricComponent;
using Wooff.ECS;

namespace Core.Components.MetricBonusComponent
{
    [Serializable]
    public class MetricBonusConfig : IConfig
    {
        public int BonusAmount;
        public int Level;
        public MetricType MetricType;
    }
}