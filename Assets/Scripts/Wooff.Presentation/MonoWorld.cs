using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Context;
using Wooff.ECS.World;

namespace Wooff.Presentation
{
    public class WorldMonoBehaviour<T> : MonoBehaviour, IWorld<IMonoEntity, IMonoComponent> where T : World<IMonoEntity, IMonoComponent>, new()
    {
        private T _world = new T();
        public IContext<IMonoEntity> EntityContext => _world.EntityContext;

        [CanBeNull] private IUpdateable _entityUpdateable;

        private void Awake()
        {
            //_entityUpdateable = EntityContext as IUpdateable;
            foreach (var monoEntity in EntityContext.Where(x => x is not null))
                EntityContextOnAddedItem(this, monoEntity);                
            
            EntityContext.ItemAdded += EntityContextOnAddedItem;
        }

        private void EntityContextOnAddedItem(object sender, IMonoEntity e)
        {
            var d = new GameObject(e.GetType().FullName);
            d.AddComponent<MonoComponentEntity>().Init(e);
        }

        private async void Update()
        {
            // await _entityUpdateable?.UpdateParallelAsync(1f)!;
            
            if (Input.GetKeyDown(KeyCode.S))
                await _world?.Save("save.json")!;

            if (Input.GetKeyDown(KeyCode.L))
                _world = await IWorld<IMonoEntity, IMonoComponent>.Load<T>("save.json");
        }
        
        public async Task Save(string fileName)        
        {
            await _world.Save(fileName);
        }
    }
}
