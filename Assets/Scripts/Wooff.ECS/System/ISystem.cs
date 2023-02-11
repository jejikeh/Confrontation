using Wooff.ECS.Context;
using Wooff.ECS.Entity;

namespace Wooff.ECS.System
{
    public interface ISystem<T> : IUpdateable<IContext<T>>
    {
    }
}