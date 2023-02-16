using Wooff.ECS.Entities;

namespace Wooff.ECS.Components
{
    public abstract class Component<T> : IComponent<T> where T : IConfig
    {
        public T Config { get; private set; }
        public IEntity Handler { get; private set; }

        public abstract int GetId();

        protected Component(T data, IEntity handler)
        {
            Config = data;
            Handler = handler;
        }
    }
}