using Wooff.ECS;
using Wooff.ECS.Components;

namespace Wooff.Presentation
{
    public interface IMonoComponent<T> : IComponent<T, IMonoEntity> where T : IConfig
    {
        
    }
}