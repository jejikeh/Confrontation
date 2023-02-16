using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Components
{
    public interface IComponent<T> : IContextItem, IConfigurable<T> where T : IConfig
    {
        public IEntity Handler { get; } 
    }
}