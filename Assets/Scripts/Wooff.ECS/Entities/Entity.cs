using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;

namespace Wooff.ECS.Entities
{
    public sealed class Entity : IEntity
    {
        private readonly Dictionary<Type, IComponent> _components;
        
        public Entity(params IComponent[] components)
        {
            _components = new Dictionary<Type, IComponent>();
            foreach (var component in components)
                _components.TryAdd(component.GetType(), component);
        }

        public IComponent ContextAdd(IComponent component)
        {
            _components.TryAdd(_components.GetType(), component);
            return component;
        }

        public T? ContextGet<T>() where T : class, IComponent
        {
            return _components[typeof(T)] as T;
        }
        
        public T? ContextGetFromInterface<T>() where T : class, IComponent
        {
            var key = _components.Keys.FirstOrDefault(x => typeof(T).IsAssignableFrom(x));
            return _components[key] as T;
        }

        public IQueryable<IComponent> GetAllComponents()
        {
            return _components.Select(x => x.Value).AsQueryable();
        }

        public IQueryable<Type> GetAllComponentTypes()
        {
            return _components.Select(x => x.Key).AsQueryable();
        }

        public bool ContextContains<T>() where T : class, IComponent
        {
            return _components.Any(x => typeof(T).IsAssignableFrom(x.Key));
        }
        
        public bool ContextRemove(IComponent component)
        {
            return _components.Remove(component.GetType());
        }

        public IQueryable<T1> ContextSelectQuery<T1>(Func<IComponent, T1> query)
        {
            return _components
                .Select(x => query.Invoke(x.Value))
                .AsQueryable();
        }

        public IQueryable<IComponent> ContextWhereQuery(Func<IComponent, bool> query)
        {
            return _components
                .Where(x => query.Invoke(x.Value))
                .Select(x => x.Value)
                .AsQueryable();
        }

        public IContextQueryable<IComponent> CreateEmptySelf()
        {
            return new Entity();
        }

        public IEnumerator<IComponent> GetEnumerator()
        {
            return _components.Select(x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}