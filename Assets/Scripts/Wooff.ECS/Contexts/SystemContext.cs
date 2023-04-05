using System.Collections.Generic;
using System.Linq;
using Wooff.ECS.Entities;
using Wooff.ECS.Systems;

namespace Wooff.ECS.Contexts 
{
    public class SystemContext : IContext<ISystem>, IProcessable<IContext<IEntity>>
    {
        private readonly HashSet<ISystem> _systems;

        public SystemContext(params ISystem[] systems)
        {
            _systems = new HashSet<ISystem>();
            foreach (var system in systems)
                ContextAdd(system);
        }
        
        public ISystem ContextAdd(ISystem item)
        {
            _systems.Add(item);
            return item;
        }

        public T1? ContextGet<T1>() where T1 : class, ISystem
        {
            return _systems.FirstOrDefault(x => x.GetType().IsAssignableFrom(typeof(T1))) as T1;
        }

        public bool ContextContains<T1>() where T1 : class, ISystem
        {
            return ContextGet<T1>() is null;
        }

        public bool ContextRemove(ISystem item)
        {
            return _systems.Remove(item);
        }

        public void Start(IContext<IEntity> context)
        {
            foreach (var system in _systems)
                system.Start(context);
        }

        public void Update(float timeScale, IContext<IEntity> context)
        {
            foreach (var system in _systems)
                system.Update(timeScale, context);
        }
    }
}