using System.Collections.Generic;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.SmoothLookAtTargetComponent;
using UnityEngine;
using UnityEngine.EventSystems;
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
        
        private static List<RaycastResult> GetEventSystemRaycastResults()
        {   
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll( eventData, raycastResults );
            return raycastResults;
        }

        private void Update()
        {
            if (_click.Config.CurrentActiveLayer == ClickLayer.Game)
                _smoothLookAtTarget.UpdateOffset(-Input.mouseScrollDelta.y);
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    _click.SetActiveLayer(ClickLayer.UI);
                    foreach (var raycastResult in GetEventSystemRaycastResults())
                        if (raycastResult.gameObject.transform.gameObject.TryGetComponent(out MonoEntity monoEntityUI))
                            _click.StartClick(monoEntityUI.ContextContains<Clickable>()
                                ? monoEntityUI.ContextGet<Clickable>()
                                : null);
                }
                else
                    _click.SetActiveLayer(ClickLayer.Game);
                
                var mousePosition = Input.mousePosition;
                var ray = _camera.ScreenPointToRay(mousePosition);
                
                if (!Physics.Raycast(ray, out RaycastHit hit)) 
                    return;

                if (hit.transform.gameObject.TryGetComponent(out MonoEntity monoEntity))
                    _click.StartClick(monoEntity.ContextContains<Clickable>()
                        ? monoEntity.ContextGet<Clickable>()
                        : null);
            }
        }
    }
}