using System.Collections.Generic;

namespace Wooff.ECS.Contexts 
{
    public interface IContext<T, out T1> where T : IContextItem where T1 : ICollection<T>
    {
        public T1 Items { get; }
        public T ContextAdd(T item);
        public T2 ContextGet<T2>() where T2 : class, T;
        public bool ContextRemove(T item);
        public bool ContextContains<T2>() where T2 : class, T;
    }
}