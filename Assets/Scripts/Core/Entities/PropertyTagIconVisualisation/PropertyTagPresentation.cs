using Core.Entities.Camera;
using DG.Tweening;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.PropertyTagIconVisualisation
{
    public class PropertyTagPresentation : MonoEntity
    {
        private SmoothCamera _smoothCamera;
        private const float _heightOffset = 1f;
        private void Start()
        {
            _smoothCamera = StaticMonoWorldFinder.GetEntity<SmoothCamera>();
            var vectorHeightOffset = new Vector3(0, _heightOffset, 0);
            var transformTag = transform;
            transformTag.DOPunchScale(Vector3.up, 1f);
            transformTag.DOScale(Vector3.one, 0.25f);
            // this fix bug when icon move bottom 
            transformTag.transform.localScale = Vector3.one;

            transformTag.DOComplete();
            transformTag
                .DOMoveY(transformTag.position.y + 0.3f, 2)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
        private void LateUpdate() {
            transform.LookAt(
                transform.position + 
                _smoothCamera.transform.forward);
        }
    }
}