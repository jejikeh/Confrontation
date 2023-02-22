using System.Collections.Generic;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Components.MetricBonusComponent.MetricBonusManager
{
    public class MetricBonusesHandler : Component<MetricBonusesHandlerConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>, IListContext<IMetricBonus>
    {
        IConfig IConfigurable<IConfig>.Config => Config;
        private readonly MetricsContext _metricsContext;
        
        public MetricBonusesHandler(MetricBonusesHandlerConfig data, IMonoEntity handler) : base(data, handler)
        {
            _metricsContext = new MetricsContext();
            foreach (var metric in data.MetricBonusConfigs)
                ContextAdd(new MetricBonus(metric));
        }

        public List<IMetricBonus> Items => _metricsContext.Items;

        public IMetricBonus ContextAdd(IMetricBonus item)
        {
            return _metricsContext.ContextAdd(item);
        }
        
        public T2 ContextGet<T2>() where T2 : class, IMetricBonus
        {
            return _metricsContext.ContextGet<T2>();
        }

        public bool ContextRemove(IMetricBonus item)
        {
            return _metricsContext.ContextRemove(item);
        }

        public bool ContextContains<T2>() where T2 : class, IMetricBonus
        {
            return _metricsContext.ContextContains<T2>();
        }
    }
}