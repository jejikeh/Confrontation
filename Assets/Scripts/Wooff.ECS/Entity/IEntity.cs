using Wooff.ECS.Component;
using Wooff.ECS.Context;

namespace Wooff.ECS.Entity
{

    public interface IEntity : IContext<IComponent>, IUpdateable, IInitable
    {
    }

    public interface IEntity<T> : IEntity, IInitable<T>
    {
    }

    public interface IEntity<T, T1> : IEntity, IInitable<T, T1>
    {
    }

    public interface IEntity<T, T1, T2> : IEntity, IInitable<T, T1, T2>
    {
    }

    public interface IEntity<T, T1, T2, T3> : IEntity, IInitable<T, T1, T2, T3>
    {
    }

    public interface IEntity<T, T1, T2, T3, T4> : IEntity, IInitable<T, T1, T2, T3, T4>
    {
    }
}