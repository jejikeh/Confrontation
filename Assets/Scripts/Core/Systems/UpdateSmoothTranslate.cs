using System.Collections.Generic;
using System.Linq;
using Core.Components.SmoothTranslateComponent;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class UpdateSmoothTranslate : IMonoSystem
    {
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            foreach (var smoothTranslate in data.Items
                         .Where(x => x.ContextContains<SmoothTranslate>())
                         .Select(x => x.ContextGet<SmoothTranslate>()))
            {
                smoothTranslate.UpdatePosition(timeScale);
                smoothTranslate.UpdateVelocity(timeScale);
            }
        }
    }
}