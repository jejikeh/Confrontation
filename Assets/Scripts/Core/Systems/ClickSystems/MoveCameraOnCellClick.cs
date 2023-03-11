using System.Linq;
using Core.Components.CellRelated;
using Core.Components.Tags;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems.ClickSystems
{
    public class CameraMoveOnClick : HandleClickedState<CellComponent>
    {
        private UnityGameObjectComponent _cameraTransformWrapperComponent;
        private SmoothTranslateComponent _smoothTranslateComponent;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var camera = context
                .ContextGetAllFromMap(typeof(CameraHandlerTagComponent))
                .FirstOrDefault()
                .ContextGet<CameraHandlerTagComponent>();

            _cameraTransformWrapperComponent = camera.UnityGameObjectComponent;
            _smoothTranslateComponent = camera.SmoothTranslateComponent;
        }

        protected override void ProcessClickedItems(EntityContext context, IEntity[] requiredEntities)
        {
            foreach (var clickedEntity in requiredEntities)
            {
                var movePointPosition = clickedEntity.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform.position;
                    
                _smoothTranslateComponent.SetPosition(
                    movePointPosition,
                    _cameraTransformWrapperComponent.UnitySceneObject.transform);
            }
        }
    }
}