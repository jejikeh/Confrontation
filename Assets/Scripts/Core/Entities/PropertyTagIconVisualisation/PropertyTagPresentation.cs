using Core.Entities.Camera;
using DG.Tweening;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.PropertyTagIconVisualisation
{
    public class PropertyTagPresentation : MonoEntity
    {
        private SmoothCamera _smoothCamera;
        private async void Start()
        {
            _smoothCamera = StaticMonoWorldFinder.GetEntity<SmoothCamera>();
            var transformTag = transform;
            transformTag.DOPunchScale(Vector3.up, 1f);
            transformTag.DOScale(Vector3.one, 0.25f);
            transformTag.transform.localScale = Vector3.one;
        }
        private void LateUpdate() {
            transform.LookAt(
                transform.position + 
                _smoothCamera.transform.forward);
        }
    }
}