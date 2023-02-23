using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;


// TODO: Add field _context and replace all methods by _context.method()

namespace Wooff.MonoIntegration
{
    public class MonoEntity : MonoBehaviour, IMonoEntity
    {
        public HashSet<IComponent<IConfig, IMonoEntity>> Items { get; } = new HashSet<IComponent<IConfig, IMonoEntity>>();
        public IComponent<IConfig, IMonoEntity> ContextAdd(IComponent<IConfig, IMonoEntity> item)
        {
            Items.Add(item);
            return item;
        }

        public T2 ContextGet<T2>() where T2 : class, IComponent<IConfig, IMonoEntity>
        {
            var item = Items.FirstOrDefault(x => x.GetType() == typeof(T2));
            return item as T2;
        }

        public bool ContextRemove(IComponent<IConfig, IMonoEntity> item)
        {
            item.OnRemove();
            return Items.Remove(item);
        }

        public bool ContextContains<T2>() where T2 : class, IComponent<IConfig, IMonoEntity>
        {
            return Items.FirstOrDefault(x => x.GetType() == typeof(T2)) is not null;
        }
        
        public T2 ContextGetAs<T2>() where T2 : class, IComponent<IConfig, IMonoEntity>
        {
            foreach (var component in Items)
                if (component is T2 templateComponent)
                    return templateComponent;

            return default;
        }

        public GameObject MonoObject { get; set; }

        protected virtual void Awake()
        {
            MonoObject = gameObject;
        }
    }
}