using Core.Components.Metrics.MetricComponent;

namespace Core.Components.Metrics.MetricMinerComponent
{
    public class MetricMiner : IMetricMiner
    {
        private readonly MetricMinerConfig _metricMinerConfig; 
        public MetricMiner(MetricMinerConfig config)
        {
            _metricMinerConfig = config;
        }
        
        public int GetBonusAmount()
        {
            return _metricMinerConfig.BonusAmount;
        }

        public MetricType MetricType => _metricMinerConfig.MetricType;
    }
}