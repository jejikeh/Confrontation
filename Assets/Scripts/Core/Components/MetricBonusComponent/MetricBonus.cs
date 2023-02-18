using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.MetricBonusComponent
{
    public class MetricBonus : Component<MetricBonusConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        IConfig IConfigurable<IConfig>.Config => Config;

        public int GetBonusAmount()
        {
            return Config.Level * Config.BonusAmount;
        }

        public MetricBonus(MetricBonusConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}