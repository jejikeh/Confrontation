using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.Metrics
{
    public class SpeedCreationUnitsMetric : Component<IConfig, IMonoEntity>
    {
        public int Speed { get; private set; }

        public SpeedCreationUnitsMetric(IConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        public void AddGold(int amount)
        {
            Speed += amount;
        }
    }
}