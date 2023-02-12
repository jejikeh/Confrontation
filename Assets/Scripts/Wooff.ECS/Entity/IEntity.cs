using System;
using Wooff.ECS.Component;
using Wooff.ECS.Context;

namespace Wooff.ECS.Entity
{
    public interface IEntity : IEntity<IComponent>
    {
    }

    public interface IEntity<T> : IContext<T>, IInitable where T : IComponent
    {
    }

    public interface IEntity<T, T1> : IEntity<T>, IInitable<T1> where T : IComponent
    {
    }
}