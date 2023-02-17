using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.SmoothLookAtTargetComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class SmoothCamera : MonoEntity
    {
        [SerializeField] private SmoothLookAtTargetConfig _smoothLookAtTargetConfig;
        private SmoothLookAtTarget _smoothLookAtTarget;
        private Click _click;
        private UnityEngine.Camera _camera;
        
        private void Start()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _smoothLookAtTarget = (SmoothLookAtTarget)ContextAdd(new SmoothLookAtTarget(_smoothLookAtTargetConfig, this));
            _click = (Click)ContextAdd(new Click(new ClickConfig
            {
                CurrentActiveLayer = ClickLayer.Game,
                LastClickable = null
            }, this));
        }

        private void Update()
        {
            _smoothLookAtTarget.UpdateOffset(Input.mouseScrollDelta.y * 10f);
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Input.mousePosition;
                var ray = _camera.ScreenPointToRay(mousePosition);
                
                if (!Physics.Raycast(ray, out RaycastHit hit)) 
                    return;
                
                var monoEntity = hit.transform.gameObject.GetComponent<MonoEntity>();
                if (monoEntity.ContextContains<Clickable>())
                    _click.StartClick(monoEntity.ContextGet<Clickable>());
            }
        }
    }
}