using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Components.Tags.UiTags;
using Core.Components.UiRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class UiRelatedComponentsInitialization : Wooff.ECS.Systems.System
    {
        private WindowContextTagComponent _windowContextTagComponent;
        private List<IEntity> _cachedEntities = new List<IEntity>();
        private int _cachedCount;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var canvasEntity = context
                .ContextGetAllFromMap(typeof(WindowContextTagComponent))
                .FirstOrDefault();

            _windowContextTagComponent = canvasEntity.ContextGet<WindowContextTagComponent>();
            _windowContextTagComponent.UnityCanvasComponent.InitCanvas(_windowContextTagComponent.UnityGameObjectComponent.UnitySceneObject.GetComponent<Canvas>());
            
            SetParentForWindowComponents(context);
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            SetParentForWindowComponents(context);
        }

        private async void SetParentForWindowComponents(EntityContext context)
        {
            if (_cachedCount == context.CountInterface<IWindowComponent>())
                return;
            
            var newEntities = context
                .ContextWhereQuery(x => x.ContextContains<IWindowTagComponent>())
                .Where(x => !_cachedEntities.Contains(x))
                .ToArray();

            _cachedEntities.AddRange(newEntities);
            _cachedCount = context.Count<IWindowComponent>();

            foreach (var entity in newEntities)
            {
                var unityObjects = entity.ContextGet<UnityGameObjectComponent>();
                unityObjects.UnitySceneObject.transform.SetParent(_windowContextTagComponent.UnityGameObjectComponent.UnitySceneObject.transform);
                unityObjects.UnitySceneObject.transform.localPosition = Vector3.zero;
                unityObjects.UnitySceneObject.transform.localScale = Vector3.zero;

                await entity.ContextGetFromInterface<IWindowComponent>()
                    .OnOpen(unityObjects.UnitySceneObject.transform);
                
                await Task.WhenAll();
            }
        }
    }
}