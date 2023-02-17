using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.Metrics
{
    public class GoldMetric : Component<IConfig, IMonoEntity>
    {
        public int GoldAmount { get; private set; }

        public GoldMetric(IConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        public void AddGold(int amount)
        {
            GoldAmount += amount;
        }
    }
}