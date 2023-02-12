using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Context;
using Wooff.ECS.System;
using Wooff.ECS.World;

namespace Wooff.Presentation
{
    public class MonoWorld<T> : MonoBehaviour, IWorld<IMonoEntity, IMonoComponent, IContext<ISystem<IMonoEntity>>> where T : World<IMonoEntity, IMonoComponent, IContext<ISystem<IMonoEntity>>>, new()
    {
        private T _world;
        public IContext<IMonoEntity> EntityContext => _world.EntityContext;
        public IContext<ISystem<IMonoEntity>> SystemContext => _world.SystemContext;

        [CanBeNull] private IUpdateable _entityUpdateable;
        [CanBeNull] private IUpdateable<IContext<IMonoEntity>> _systemUpdateable;
        [CanBeNull] private IStartable<IContext<IMonoEntity>> _systemStartable;

        private void Awake()
        {
            _world = new T();
            _entityUpdateable = EntityContext as IUpdateable;
            _systemUpdateable = SystemContext as IUpdateable<IContext<IMonoEntity>>;
            _systemStartable = SystemContext as IStartable<IContext<IMonoEntity>>;
            
            _world.EntityContext.ItemAdded += EntityContextOnItemAdded;
        }
            
        private void Start()
        {
            _world.Initialize();
            _systemStartable?.StartOneThread(EntityContext);
        }

        private void InitNewEntities()
        {
            foreach (var entity in EntityContext)
                EntityContextOnItemAdded(this, entity);
            
            _systemStartable?.StartOneThread(EntityContext);
        }

        private void EntityContextOnItemAdded(object sender, IMonoEntity entity)
        {
            var monoEntityGameObject = new GameObject(
                entity.GetType().FullName,
                entity.GetType());
            entity.MonoObject = monoEntityGameObject;
            entity.MonoObject.transform.SetParent(transform);
        }

        private async void Update()
        {
            _systemUpdateable?.UpdateOneThread(1f, EntityContext);
            
            if (Input.GetKeyDown(KeyCode.S))
                await _world?.Save("save.json")!;

            if (Input.GetKeyDown(KeyCode.L))
            {
                _world = await IWorld<IMonoEntity, IMonoComponent, IContext<ISystem<IMonoEntity>>>.Load<T>("save.json");
                _world.Initialize();
                InitNewEntities();
            }
        }
        
        public async Task Save(string fileName)        
        {
            await _world.Save(fileName);
        }
    }
}
