using System.Collections.Generic;
using System.Linq;
using Core.Components.Metrics.MetricComponent;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Components.Metrics.MetricMinerComponent.MetricMinerManager
{
    public class MetricMinerHandler : 
        Component<MetricMinerHandlerConfig, IMonoEntity>, 
        IComponent<IConfig, IMonoEntity>, 
        IListContext<IMetricMiner>
    {
        IConfig IConfigurable<IConfig>.Config => Config;
        private readonly MetricMinerContext _metricMinerContext;
        public List<IMetricMiner> Items => _metricMinerContext.Items;

        
        public MetricMinerHandler(MetricMinerHandlerConfig data, IMonoEntity handler) : base(data, handler)
        {
            _metricMinerContext = new MetricMinerContext();
            foreach (var metric in data.MetricBonusConfigs)
                ContextAdd(new MetricMiner(metric));
        }

        public IMetricMiner GetMetricMiner(MetricType metricType)
        {
            return _metricMinerContext.Items.FirstOrDefault(x => x.MetricType == metricType);
        }

        public bool ContainsMetric(MetricType metricType)
        {
            return _metricMinerContext.Items.Any(x => x.MetricType == metricType);
        }
        
        public IMetricMiner ContextAdd(IMetricMiner item)
        {
            return _metricMinerContext.ContextAdd(item);
        }
        
        public T2 ContextGet<T2>() where T2 : class, IMetricMiner
        {
            return _metricMinerContext.ContextGet<T2>();
        }

        public bool ContextRemove(IMetricMiner item)
        {
            return _metricMinerContext.ContextRemove(item);
        }

        public bool ContextContains<T2>() where T2 : class, IMetricMiner
        {
            return _metricMinerContext.ContextContains<T2>();
        }
    }
}