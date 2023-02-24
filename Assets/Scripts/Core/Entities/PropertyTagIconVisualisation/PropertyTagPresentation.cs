using Core.Entities.Camera;
using Wooff.MonoIntegration;

namespace Core.Entities.PropertyTagIconVisualisation
{
    public class PropertyTagPresentation : MonoEntity
    {
        private SmoothCamera _smoothCamera;
        private void Start()
        {
            _smoothCamera = FindObjectOfType<MonoWorld>().GetEntity<SmoothCamera>();
        }
        private void LateUpdate() {
            transform.LookAt(
                transform.position + 
                _smoothCamera.transform.forward);
        }
    }
}