using Wooff.ECS;
using Wooff.ECS.Component;

namespace Wooff.Presentation
{
    public interface IMonoComponent : IUpdatableComponent
    {
        
    }
    
    public interface IMonoComponent<T> : IMonoComponent, IUpdatableComponent<T>
    {
        
    }
}