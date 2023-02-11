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

        public int CompareTo(IEntity other)
        {
            if (other is not Entity helloWorld)
                return -1;

            if (helloWorld == this)
                return 0;

            return -1;
        }

        public IInitable Init()
        {
            return this;
        }
    }

    public abstract class Entity<T> : UpdateableContext<T>, IEntity<T> where T : IComponent, IUpdatableComponent, IComparable<T>
    {
        public IInitable Init()
        {
            return this;
        }

        public int CompareTo(IEntity other)
        {
            if (other is not { } helloWorld)
                return -1;

            if (helloWorld == this as IEntity)
                return 0;

            return -1;
        }
    }
}