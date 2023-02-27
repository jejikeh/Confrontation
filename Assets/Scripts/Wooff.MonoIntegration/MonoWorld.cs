using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Cells;
using Core.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wooff.ECS;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.ECS.Systems;
using Wooff.ECS.Worlds;

namespace Wooff.MonoIntegration
{
    public class MonoWorld : MonoBehaviour, IWorld<IMonoEntity, IMonoSystem>
    {
        public IContext<IMonoEntity, List<IMonoEntity>> EntityContext { get; } = new EntityContext<IMonoEntity>();
        public IContext<IMonoSystem, HashSet<IMonoSystem>> SystemContext { get; } = new SystemContext<IMonoSystem, IMonoEntity>();

        private void Awake()
        {
            SystemContext.ContextAdd(new SmoothLookAt());
            SystemContext.ContextAdd(new SmoothRotateAround());
            SystemContext.ContextAdd(new UpdateSmoothTranslate());
            SystemContext.ContextAdd(new MetricsBonuses());
            SystemContext.ContextAdd(new CameraTranslateToLastClickedGameItem());
            SystemContext.ContextAdd(new DrawMetricText());
            SystemContext.ContextAdd(new PlayerOwnerIconsVisualization());
            SystemContext.ContextAdd(new UserPlayerBuyCell());
            SystemContext.ContextAdd(new TurnToMove());

            foreach (var monoEntity in FindObjectsByType<MonoEntity>(FindObjectsSortMode.None))
                EntityContext.ContextAdd(monoEntity);
            
            foreach (var monoEntity in FindObjectsByType<StaticMonoEntity<MonoBehaviour>>(FindObjectsSortMode.None))
                EntityContext.ContextAdd(monoEntity);
        }
        
        public T SpawnEntity<T>(string nameObject) where T : MonoEntity
        {
            var obj = new GameObject(nameObject);
            var monoEntity = obj.AddComponent<T>();
            EntityContext.ContextAdd(monoEntity);
            return monoEntity;
        }
        
        public T SpawnEntity<T>(GameObject prefab) where T : MonoEntity
        {
            var obj = Instantiate(prefab);
            var fullName = typeof(T).FullName;
            if (fullName != null) 
                obj.name = fullName;
            var monoEntity = obj.AddComponent<T>();
            EntityContext.ContextAdd(monoEntity);
            return monoEntity;
        }
        
        public T SpawnEntity<T>(GameObject prefab, Vector3 position) where T : MonoEntity
        {
            var obj = new GameObject(typeof(T).FullName);
            var monoEntity = obj.AddComponent<T>();
            monoEntity.transform.position = position;
            _ = Instantiate(prefab, monoEntity.transform);
            EntityContext.ContextAdd(monoEntity);
            return monoEntity;
        }

        public CellWorldCreator FindCellWorldCreator()
        {
            return FindObjectOfType<CellWorldCreator>();
        }
        
        
        public T SpawnEntity<T>(IMonoEntity parent, GameObject prefab) where T : MonoEntity
        {
            var monoEntity = Instantiate(prefab, parent.MonoObject.transform).GetComponent<T>();
            EntityContext.ContextAdd(monoEntity);
            return monoEntity;
        }

        public List<T> FindEntities<T>() where T : MonoEntity
        {
            
            return EntityContext.Items
                .Where(x => x.MonoObject != null)
                .Where(x => x.GetType() == typeof(T))
                .Select(x => x as T)
                .ToList();
        }
        
        public T GetEntity<T>() where T : MonoEntity
        {
            return EntityContext.ContextGet<T>();
        }
        
        public void DestroyAllChildren(IMonoEntity entity)
        {
            foreach (Transform child in entity.MonoObject.transform)
                Destroy(child.gameObject);
        }

        public void DestroyEntity(IMonoEntity entity)
        {   
            EntityContext.ContextRemove(entity);
            DestroyImmediate(entity.MonoObject);
        }

        public void AttachPrefabToEntity(GameObject prefab, IMonoEntity entity)
        {
            Instantiate(prefab, entity.MonoObject.transform);
        }

        private void Update()
        {
            (SystemContext as IProcessable<IContext<IMonoEntity, List<IMonoEntity>>>)?.Process(1f, EntityContext);
            if (Input.GetKeyDown(KeyCode.K))
                SceneManager.LoadScene("BenchmarkECS");
            if (Input.GetKeyDown(KeyCode.T))
                SceneManager.LoadScene("Benchma");
        }

        private void OnDestroy()
        {
            for (var i = 0; i < EntityContext.Items.Count; i++)
                EntityContext.ContextRemove(EntityContext.Items[i]);
            
            SystemContext.Items.Clear();
        }
    }
}
