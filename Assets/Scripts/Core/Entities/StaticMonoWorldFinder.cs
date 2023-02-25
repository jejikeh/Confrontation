using System.Collections.Generic;
using Core.Entities.Cells;
using JetBrains.Annotations;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities
{
    public class StaticMonoWorldFinder : StaticMonoEntity<StaticMonoWorldFinder>
    {
        private MonoWorld _world;
        protected override void Awake()
        {
            base.Awake();
            _world = new GameObject("MonoWorld").AddComponent<MonoWorld>();
        }

        public static T SpawnEntity<T>(GameObject prefab, Vector3 position) where T : MonoEntity
        {
            return Instance._world.SpawnEntity<T>(prefab, position);
        }

        public static T SpawnEntity<T>(GameObject prefab) where T : MonoEntity
        {
            return Instance._world.SpawnEntity<T>(prefab);
        }


        [CanBeNull]
        public static T GetEntity<T>() where T : MonoEntity
        {
            return FindObjectOfType<MonoWorld>().GetEntity<T>();
        }

        public static T SpawnEntity<T>(string nameObject) where T : MonoEntity
        {
            // Toже самое что и в FindEntities
            return FindObjectOfType<MonoWorld>().SpawnEntity<T>(nameObject);
        }

        public static List<T> FindEntities<T>() where T : MonoEntity
        {
            // Это обходит странный баг, когда при запросе к полю _world по какой-то причине выдает NullReference
            // В EntityContext после перезагрузки сцены не попадают PlayerPressentation Entities, хотя они инициализируются так же как и остальные Entity
            return FindObjectOfType<MonoWorld>().FindEntities<T>();
        }

        public static T SpawnEntity<T>(IMonoEntity parent, GameObject prefab) where T : MonoEntity
        {
            return Instance._world.SpawnEntity<T>(parent, prefab);
        }

        public static void DestroyEntity(IMonoEntity entity)
        {
            Instance._world.DestroyEntity(entity);
        }

        public static void AttachPrefabToEntity(GameObject prefab, IMonoEntity entity)
        {
            Instance._world.AttachPrefabToEntity(prefab, entity);
        }

        public static void DestroyAllChildren(IMonoEntity entity)
        {
            Instance._world.DestroyAllChildren(entity);
        }

        public static CellWorldCreator FindCellWorldCreator()
        {
            return Instance._world.FindCellWorldCreator();
        }
    }
}