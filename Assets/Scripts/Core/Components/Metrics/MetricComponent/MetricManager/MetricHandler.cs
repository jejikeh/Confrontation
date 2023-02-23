using System.Collections.Generic;
using System.Linq;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Components.Metrics.MetricComponent.MetricManager
{
    public class MetricHandler : 
        Component<MetricHandlerConfig, IMonoEntity>, 
        IComponent<IConfig, IMonoEntity>, 
        IHashContext<IMetric>
    {
        IConfig IConfigurable<IConfig>.Config => Config;
        public HashSet<IMetric> Items => _metricContext.Items;
        private readonly MetricContext _metricContext;

        public MetricHandler(MetricHandlerConfig data, IMonoEntity handler) : base(data, handler)
        {
            _metricContext = new MetricContext();
        }
        
        public IMetric ContextAdd(IMetric item)
        {
            return Items.Any(metric => metric.MetricType == item.MetricType)
                ? default
                : _metricContext.ContextAdd(item);
        }

        public T2 ContextGet<T2>() where T2 : class, IMetric
        {
            return _metricContext.ContextGet<T2>();
        }

        public bool ContextRemove(IMetric item)
        {
            return _metricContext.ContextRemove(item);
        }

        public bool ContextContains<T2>() where T2 : class, IMetric
        {
            return _metricContext.ContextContains<T2>();
        }

        public IMetric GetMetricByType(MetricType metricType)
        {
            return Items.FirstOrDefault(metric => metric.MetricType == metricType);
        }
    }
}