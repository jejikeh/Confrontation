using Wooff.ECS.Components;
using Wooff.ECS.Contexts;

namespace Wooff.ECS.Entities 
{
    public interface IEntity<T> : IHashContext<IComponent<IConfig, T>>, IContextItem
        where T : IEntity<T>
    {
        
    }
}