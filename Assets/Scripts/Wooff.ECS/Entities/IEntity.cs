using Wooff.ECS.Components;
using Wooff.ECS.Contexts;

namespace Wooff.ECS.Entities 
{
    public interface IEntity : 
        IContext<IComponent>, 
        IContextQueryable<IComponent>
    {
        public T? ContextGetFromInterface<T>() where T : class, IComponent;
    }
}