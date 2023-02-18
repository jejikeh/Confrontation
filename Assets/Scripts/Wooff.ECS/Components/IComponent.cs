using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Components
{
    public interface IComponent<T, out T1> : IContextItem, IConfigurable<T> 
        where T : IConfig
        where T1 : IEntity<T1>
    {
        public T1 Handler { get; }
        public void OnRemove();
    }
}