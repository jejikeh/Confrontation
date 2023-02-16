using System.Collections.Generic;
using System.Linq;

namespace Wooff.ECS.Contexts
{
    public abstract class Context<T, T1> : IContext<T, T1> 
        where T : IContextItem
        where T1 : class, ICollection<T>, new()
    {
        public T1 Items { get; } = new T1();

        public T ContextAdd(T item)
        {
            Items.Add(item);
            return item;
        }

        public bool ContextRemove(T item)
        {
            return Items.Remove(item);
        }

        public bool ContextContains<T2>() where T2 : class, T
        {   
            return Items.FirstOrDefault(x => x.GetType() == typeof(T2)) is not null;
        }

        public T2? ContextGet<T2>() where T2 : class, T
        {
            var item = Items.FirstOrDefault(x => x.GetType() == typeof(T2));
            return item as T2;
        }
    }
}