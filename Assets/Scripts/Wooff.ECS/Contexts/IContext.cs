namespace Wooff.ECS.Contexts
{
    public interface IContext<T>
    {
        public T ContextAdd(T item);
        public T1? ContextGet<T1>() where T1 : class, T;
        public bool ContextContains<T1>() where T1 : class, T;
        public bool ContextRemove(T item);
    }
}