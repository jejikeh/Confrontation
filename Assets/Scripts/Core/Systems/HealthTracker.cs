using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Components;
using Core.Components.Tags.UiTags.Windows;
using Core.Components.UiRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class HealthTracker : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedEntities = new List<IEntity>();
        // TODO: cache not the entities count but count from map component|list entity 
        private int _cachedCount;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _cachedEntities = context
                .ContextGetAllFromMap(typeof(HealthComponent))
                .ToList();
            
            _cachedCount = context.Count();
        }

        public override async void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                context.ContextAdd(new ToolBoxWindowTagComponent(UiComponentsDataPrefabsHandler.ToolBoxTagComponentData)
                    .CreateWindowEntityContainer());
            }
            
            if (_cachedCount != context.Count<HealthComponent>())
            {
                _cachedEntities = context.ContextWhereQuery(x => x.ContextContains<HealthComponent>()).ToList();
                _cachedCount = context.Count<HealthComponent>();
            }

            foreach (var diedEntity in _cachedEntities)
            {
                if(diedEntity.ContextGet<HealthComponent>().Health > 0)
                    continue;
                
                var unityObject = diedEntity.ContextGet<UnityGameObjectComponent>();
                if (diedEntity.ContextContains<IWindowComponent>())
                    await diedEntity.ContextGetFromInterface<IWindowComponent>().OnClose(unityObject.UnitySceneObject.transform);
                
                context.ContextRemove(diedEntity);
                await Task.WhenAll();
                MonoWorld.Destroy(unityObject.UnitySceneObject);
            }
        }
    }
}