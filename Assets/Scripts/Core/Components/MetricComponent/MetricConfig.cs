using System;
using Wooff.ECS;

namespace Core.Components.MetricComponent
{
    [Serializable]
    public class MetricConfig : IConfig
    {
        public MetricType MetricType;
        public int StartAmount;
    }
}