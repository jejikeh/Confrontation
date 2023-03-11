using System.Linq;
using Core.Components;
using Core.Components.Tags;
using Core.Components.Tags.UiTags;
using Core.Components.Tags.UiTags.Windows;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class HandleClickedState : Wooff.ECS.Systems.System
    {
        private UnityGameObjectComponent _cameraTransformWrapperComponent;
        private SmoothTranslateComponent _smoothTranslateComponent;
        
        private IEntity[] _cachedEntities;
        private int _cachedCount;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var camera = context
                .ContextGetAllFromMap(typeof(CameraHandlerTagComponent))
                .FirstOrDefault()
                .ContextGet<CameraHandlerTagComponent>();

            _cameraTransformWrapperComponent = camera.UnityGameObjectComponent;
            _smoothTranslateComponent = camera.SmoothTranslateComponent;
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCount != context.Count())
            {
                _cachedEntities = context.ContextWhereQuery(x =>
                    x.ContextContains<MovePointCameraTagComponent>()).ToArray();

                _cachedCount = context.Count();
            }
            
            foreach (var clickedEntities in _cachedEntities)
            {
                var clickableComponent = clickedEntities.ContextGet<ClickableComponent>();
                if(!clickableComponent.Clicked)
                    continue;

                var movePointPosition = clickedEntities.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform.position;
                
                _smoothTranslateComponent.SetPosition(
                    movePointPosition,
                    _cameraTransformWrapperComponent.UnitySceneObject.transform);

                if (clickedEntities.ContextContains<InformationComponent>())
                {
                    var informationWindow = context.ContextGetAllFromMap(typeof(InformationWindowTagComponent)).FirstOrDefault();
                    
                    if(informationWindow is null)
                        context.ContextAdd(new InformationWindowTagComponent(clickedEntities.ContextGet<InformationComponent>(), UiComponentsDataPrefabsHandler.InformationTagComponentData).CreateWindowEntityContainer());
                    else
                        informationWindow.ContextGet<InformationWindowTagComponent>().WindowComponent.UpdateTextInformation(clickedEntities.ContextGet<InformationComponent>());
                }

                clickableComponent.StateIsHandled();
            }
        }
    }
}