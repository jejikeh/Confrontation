using System;

namespace Wooff.ECS
{

    public interface IInitable
    {
        public IInitable Init();

        public static T Initialize<T>() where T : IInitable, new()
        {
            var item = new T();
            item.Init();
            return item;
        }

        public static T Initialize<T>(Func<T> action) where T : IInitable, new()
        {
            var item = action.Invoke();
            return item;
        }
    }

// ReSharper disable TypeParameterCanBeVariant
    public interface IInitable<T> : IInitable
    {
        public IInitable<T> Init(T data);

        public static T1 Initialize<T1>(T data) where T1 : IInitable<T>, new()
        {
            var item = new T1();
            item.Init(data);
            return item;
        }
    }

    public interface IInitable<T, T1> : IInitable
    {
        public IInitable<T, T1> Init(T dataT, T1 dataT1);

        public static T2 Initialize<T2>(T data, T1 dataT1) where T2 : IInitable<T, T1>, new()
        {
            var item = new T2();
            item.Init(data, dataT1);
            return item;
        }
    }

    public interface IInitable<T, T1, T2> : IInitable
    {
        public IInitable<T, T1, T2> Init(T dataT, T1 dataT1, T2 dataT2);

        public static T3 Initialize<T3>(T data, T1 dataT1, T2 dataT2) where T3 : IInitable<T, T1, T2>, new()
        {
            var item = new T3();
            item.Init(data, dataT1, dataT2);
            return item;
        }
    }

    public interface IInitable<T, T1, T2, T3> : IInitable
    {
        public IInitable<T, T1, T2, T3> Init(T dataT, T1 dataT1, T2 dataT2, T3 dataT3);

        public static T4 Initialize<T4>(T data, T1 dataT1, T2 dataT2, T3 dataT3)
            where T4 : IInitable<T, T1, T2, T3>, new()
        {
            var item = new T4();
            item.Init(data, dataT1, dataT2, dataT3);
            return item;
        }
    }

    public interface IInitable<T, T1, T2, T3, T4> : IInitable
    {
        public IInitable<T, T1, T2, T3, T4> Init(T dataT, T1 dataT1, T2 dataT2, T3 dataT3, T4 dataT4);

        public static T5 Initialize<T5>(T data, T1 dataT1, T2 dataT2, T3 dataT3, T4 dataT4)
            where T5 : IInitable<T, T1, T2, T3, T4>, new()
        {
            var item = new T5();
            item.Init(data, dataT1, dataT2, dataT3, dataT4);
            return item;
        }
    }
}