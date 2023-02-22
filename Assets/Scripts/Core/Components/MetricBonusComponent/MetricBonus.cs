using Core.Components.MetricComponent;
using Wooff.ECS.Contexts;

namespace Core.Components.MetricBonusComponent
{
    public class MetricBonus : IMetricBonus
    {
        private readonly MetricBonusConfig _metricBonusConfig; 
        public MetricBonus(MetricBonusConfig config)
        {
            _metricBonusConfig = config;
        }
        public int GetBonusAmount()
        {
            return _metricBonusConfig.BonusAmount;
        }

        public MetricType MetricType => _metricBonusConfig.MetricType;
    }
}