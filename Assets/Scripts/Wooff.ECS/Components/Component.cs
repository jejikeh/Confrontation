using Wooff.ECS.Entities;

namespace Wooff.ECS.Components
{
    public abstract class Component<T, T1> : IComponent<T, T1>
        where T : IConfig where T1 : IEntity<T1>
    {
        public T Config { get; private set; }
        public T1 Handler { get; private set; }

        protected Component(T data, T1 handler)
        {
            Config = data;
            Handler = handler;
        }

        public virtual void OnRemove()
        {
            
        }
    }
}