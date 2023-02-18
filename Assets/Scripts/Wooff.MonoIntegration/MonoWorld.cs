using System.Collections.Generic;
using Core.Systems;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.ECS.Systems;
using Wooff.ECS.Worlds;

namespace Wooff.MonoIntegration
{
    public class MonoWorld : Singleton<MonoWorld>, IWorld<IMonoEntity, IMonoSystem>
    {
        public IContext<IMonoEntity, List<IMonoEntity>> EntityContext { get; } = new EntityContext<IMonoEntity>();
        public IContext<IMonoSystem, HashSet<IMonoSystem>> SystemContext { get; } = new SystemContext<IMonoSystem, IMonoEntity>();

        protected override void Awake()
        {
            base.Awake();
            
            SystemContext.ContextAdd(new SmoothLookAt());
            SystemContext.ContextAdd(new SmoothRotateAround());
            SystemContext.ContextAdd(new UpdateSmoothTranslate());
            SystemContext.ContextAdd(new MetricsBonuses());
            SystemContext.ContextAdd(new CameraTranslateToLastClickedGameItem());

            foreach (var monoEntity in FindObjectsByType<MonoEntity>(FindObjectsSortMode.None))
                EntityContext.ContextAdd(monoEntity);
            
            foreach (var monoEntity in FindObjectsByType<StaticMonoEntity>(FindObjectsSortMode.None))
                EntityContext.ContextAdd(monoEntity);
        }

        private T SpawnNewEntity<T>() where T : MonoEntity
        {
            var obj = new GameObject(typeof(T).FullName);
            var monoEntity = obj.AddComponent<T>();
            EntityContext.ContextAdd(monoEntity);
            return monoEntity;
        }
        
        private T SpawnNewEntity<T>(GameObject prefab) where T : MonoEntity
        {
            var obj = new GameObject();
            var monoEntity = obj.AddComponent<T>();
            _ = Instantiate(prefab, monoEntity.transform);
            EntityContext.ContextAdd(monoEntity);
            return monoEntity;
        }
        
        public static void DestroyAllChildren(IMonoEntity entity)
        {
            foreach (Transform child in entity.MonoObject.transform)
                Destroy(child.gameObject);
        }

        public static void AttachPrefabToEntity(GameObject prefab, IMonoEntity entity)
        {
            Instantiate(prefab, entity.MonoObject.transform);
        }

        public static T SpawnEntity<T>() where T : MonoEntity
        {
            return Instance.SpawnNewEntity<T>();
        }
        public static T SpawnEntity<T>(GameObject prefab) where T : MonoEntity
        {
            return Instance.SpawnNewEntity<T>(prefab);
        }

        private void Update()
        {
            (SystemContext as IProcessable<IContext<IMonoEntity, List<IMonoEntity>>>)?.Process(1f, EntityContext);
        }
    }
}
