using System.Linq;
using Core.Components;
using Core.Components.Tags;
using Core.Components.UnityRelated;
using UnityEngine;
using UnityEngine.EventSystems;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems.ClickSystems
{
    public class CameraMouseClick : Wooff.ECS.Systems.System
    {
        private UnityCameraComponent _unityCameraComponent;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var camera = context
                .ContextGetAllFromMap(typeof(CameraHandlerTagComponent))
                .FirstOrDefault();

            _unityCameraComponent = camera
                .ContextGet<UnityCameraComponent>()
                .InitCamera(
                    camera.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform.GetChild(0).GetComponent<Camera>());
        }
        
        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Если курсор находится над UI-элементом
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                
                var mousePosition = Input.mousePosition;
                var ray = _unityCameraComponent.Camera.ScreenPointToRay(mousePosition);
                
                if (!Physics.Raycast(ray, out RaycastHit hit)) 
                    return;

                if (hit.transform.gameObject.TryGetComponent(out MonoEntity monoEntity))
                    if (monoEntity.HandledEntity.ContextContains<ClickableComponent>())
                        monoEntity.HandledEntity.ContextGet<ClickableComponent>().ClickOnMe();
            }
        }
    }
}