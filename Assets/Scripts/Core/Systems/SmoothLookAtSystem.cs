using Core.Components.SmoothLookAtComponent;
using Wooff.Presentation;

namespace Core.Systems
{
    public class SmoothLookAtSystem : MonoSystem
    {
        protected override void SystemUpdateOneThread(float timeScale, IMonoEntity updateItem)
        {
            if (!updateItem.Contains<SmoothLookAt>())
                return;
            
            updateItem.GetFirstNullable<SmoothLookAt>()?.UpdateLookAt(updateItem);
        }
    }
}