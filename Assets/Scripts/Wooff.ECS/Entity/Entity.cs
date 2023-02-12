using System;
using Wooff.ECS.Component;
using Wooff.ECS.Context;

namespace Wooff.ECS.Entity
{
    public class Entity : UpdateableContext<IUpdatableComponent>, IEntity<IUpdatableComponent>
    {
        public new IUpdatableComponent Add(IUpdatableComponent item)
        {
            if (Contains(item))
                throw new ArgumentException(
                    $"Component {item.GetType().FullName} is already attached to entity {GetType().FullName}");

            base.Add(item);
            return item;
        }

        public new T1 Add<T1>() where T1 : IUpdatableComponent, new()
        {
            var item = IInitable.Initialize<T1>() as IUpdatableComponent;
            if (item is not T1 parsedComponent || Contains<T1>())
                throw new ArgumentException(
                    $"Component {item?.GetType().FullName} is already attached to entity {GetType().FullName}");

            Add(parsedComponent);
            return parsedComponent;
        }

        public new T1 Add<T1, T2>(T2 data) where T1 : IUpdatableComponent, IInitable<T2>, new()
        {
            var item = IInitable<T2>.Initialize<T1>(data) as IUpdatableComponent;
            if (item is not T1 parsedComponent || Contains<T1>())
                throw new ArgumentException(
                    $"Component {item?.GetType().FullName} is already attached to entity {GetType().FullName}");

            Add(parsedComponent);
            return parsedComponent;
        }

        public IInitable Init()
        {
            return this;
        }
    }

    public abstract class Entity<T> : UpdateableContext<T>, IEntity<T> where T : IComponent, IUpdatableComponent
    {
        public new IUpdatableComponent Add(T item)
        {
            if (Contains(item))
                throw new ArgumentException(
                    $"Component {item.GetType().FullName} is already attached to entity {GetType().FullName}");

            base.Add(item);
            return item;
        }

        public new T1 Add<T1>() where T1 : T, new()
        {
            var item = IInitable.Initialize<T1>() as IUpdatableComponent;
            if (item is not T1 parsedComponent || Contains<T1>())
                throw new ArgumentException(
                    $"Component {item?.GetType().FullName} is already attached to entity {GetType().FullName}");

            Add(parsedComponent);
            return parsedComponent;
        }

        public new T1 Add<T1, T2>(T2 data) where T1 : T, IInitable<T2>, new()
        {
            var item = IInitable<T2>.Initialize<T1>(data) as IUpdatableComponent;
            if (item is not T1 parsedComponent || Contains<T1>())
                throw new ArgumentException(
                    $"Component {item?.GetType().FullName} is already attached to entity {GetType().FullName}");

            Add(parsedComponent);
            return parsedComponent;
        }
        
        public IInitable Init()
        {
            return this;
        }
    }

    public abstract class Entity<T, T1> : Entity<T>, IEntity<T, T1> where T : IUpdatableComponent, IComparable<T>
    {
        protected T1 Data;

        public IInitable<T1> Init(T1 data)
        {
            Data = data;
            return this;
        }
    }
}