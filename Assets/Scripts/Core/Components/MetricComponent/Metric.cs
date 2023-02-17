using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.MetricComponent
{
    public class Metric : Component<MetricConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        public int Amount { get; private set; }
        IConfig IConfigurable<IConfig>.Config => Config;
        
        public Metric(MetricConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        public void AddToMetric(int amount)
        {
            Amount += amount;
        }
    }
}