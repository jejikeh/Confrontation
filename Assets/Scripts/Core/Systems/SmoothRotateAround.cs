using System.Collections.Generic;
using System.Linq;
using Core.Components.SmoothRotateComponent;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class SmoothRotateAround : IMonoSystem
    {
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            foreach (var smoothRotate in data.Items
                         .Where(x => x.ContextContains<SmoothRotate>())
                         .Select(x => x.ContextGet<SmoothRotate>()))
            {
                smoothRotate.Update(timeScale);
            }
        }
    }
}