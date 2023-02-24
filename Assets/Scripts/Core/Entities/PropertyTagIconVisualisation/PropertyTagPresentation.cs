using Core.Entities.Camera;
using Wooff.MonoIntegration;

namespace Core.Entities.PropertyTagIconVisualisation
{
    public class PropertyTagPresentation : MonoEntity
    {
        private void LateUpdate() {
            transform.LookAt(transform.position + MonoWorld.GetEntity<SmoothCamera>().transform.forward);
        }
    }
}