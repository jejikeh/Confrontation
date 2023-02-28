using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.SmoothLookAtTargetComponent
{
    public class TargetLootAt : Component<IConfig,IMonoEntity>
    {
        public TargetLootAt(IConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}