using System.Collections.Generic;
using System.Linq;
using Core.Components.SmoothLookAtTargetComponent;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class SmoothLookAt : IMonoSystem
    {
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            foreach (var smoothLookAtTarget in data.Items
                         .Where(x => x.ContextContains<SmoothLookAtTarget>())
                         .Select(x => x.ContextGet<SmoothLookAtTarget>()))
            {
                smoothLookAtTarget.LookAt(
                    timeScale,
                    data.Items.FirstOrDefault(x => x.ContextContains<TargetLootAt>()));
            }
        }
    }
}