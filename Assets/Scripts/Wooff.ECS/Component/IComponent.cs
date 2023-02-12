using System;

namespace Wooff.ECS.Component
{
    public interface IComponent : IInitable
    {
        
    }

    public interface IUpdatableComponent : IComponent, IUpdateable
    {
        
    }

    public interface IComponent<T> : IComponent, IInitable<T>
    {
        
    }

    public interface IUpdatableComponent<T> : IComponent<T>, IUpdateable
    {
        
    }
    
    public interface IUpdatableComponent<T, in T1> : IComponent<T>, IUpdateable<T1>
    {
        
    }
}