using System.Linq;
using Core.Components;
using Core.Components.CellRelated;
using Core.Components.Tags;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems.ClickSystems
{
    public class MoveCameraOnCellClick : HandleClickedState<CellTagComponent>
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

        protected override void ProcessClickedEntity(EntityContext context, IEntity clickedEntity)
        {
            var movePointPosition = clickedEntity.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform
                .position;

            _smoothTranslateComponent.SetPosition(
                movePointPosition,
                _cameraTransformWrapperComponent.UnitySceneObject.transform);
        }
    }
}