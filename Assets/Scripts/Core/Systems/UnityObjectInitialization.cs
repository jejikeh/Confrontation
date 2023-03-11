using System.Collections.Generic;
using System.Linq;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class UnityObjectInitialization : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedEntities = new List<IEntity>();
        // TODO: cache not the entities count but count from map component|list entity 
        private int _cachedCount;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            InitializeGameObjectComponent(context);
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            // На случай добавления нового существа после Start
            InitializeGameObjectComponent(context);
        }

        private void InitializeGameObjectComponent(EntityContext context)
        {
            if (_cachedCount == context.Count<UnityGameObjectComponent>()) 
                return;
            
            var newEntities = context
                .ContextWhereQuery(x => x.ContextContains<UnityGameObjectComponent>())
                .Where(x => !_cachedEntities.Contains(x))
                .ToArray();

            _cachedEntities.AddRange(newEntities);
            _cachedCount = context.Count<UnityGameObjectComponent>();
            
            foreach (var entity in newEntities)
            {
                var unityGameObject = entity.ContextGet<UnityGameObjectComponent>();
                var unitySpawnedInSceneObject = MonoWorld.SpawnEntity(unityGameObject.UnityPrefab);
                
                if (unitySpawnedInSceneObject.TryGetComponent<MonoEntity>(out var monoEntity))
                    monoEntity.HandledEntity = entity;
                else
                    unitySpawnedInSceneObject.AddComponent<MonoEntity>().HandledEntity = entity;
                    
                unityGameObject.UnitySceneObject = unitySpawnedInSceneObject;
                unityGameObject.InitPositionAndRotation();
            }
        }
    }
}   
