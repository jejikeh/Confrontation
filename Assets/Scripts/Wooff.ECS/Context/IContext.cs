using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Wooff.ECS.Context
{

    public interface IContext<T> : ICollection<T>
    {
        public T1 Add<T1>() where T1 : T, new();
        public T1 Add<T1>(Func<T1> action) where T1 : T, IInitable, new();
        public T1 Add<T1, T2>(T2 data) where T1 : T, IInitable<T2>, new();
        public T1 Add<T1, T2, T3>(T2 dataT, T3 dataT1) where T1 : T, IInitable<T2, T3>, new();
        public T1 Add<T1, T2, T3, T4>(T2 dataT, T3 dataT1, T4 dataT2) where T1 : T, IInitable<T2, T3, T4>, new();

        public T1 Add<T1, T2, T3, T4, T5>(T2 dataT, T3 dataT1, T4 dataT2, T5 dataT3)
            where T1 : T, IInitable<T2, T3, T4, T5>, new();

        public T1 Add<T1, T2, T3, T4, T5, T6>(T2 dataT, T3 dataT1, T4 dataT2, T5 dataT3, T6 dataT4)
            where T1 : T, IInitable<T2, T3, T4, T5, T6>, new();

        public T1 Add<T1, T2, TD2>(TD2 dataT)
            where T1 : T, IInitable<T2>, new()
            where T2 : IInitable<TD2>, new();

        public T1 Add<T1, T2, TD2, T3, TD3>(TD2 dataT, TD3 dataT1)
            where T1 : T, IInitable<T2, T3>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new();

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4>(TD2 dataT, TD3 dataT1, TD4 dataT2)
            where T1 : T, IInitable<T2, T3, T4>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new()
            where T4 : IInitable<TD4>, new();

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5>(TD2 dataT, TD3 dataT1, TD4 dataT2, TD5 dataT3)
            where T1 : T, IInitable<T2, T3, T4, T5>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new()
            where T4 : IInitable<TD4>, new()
            where T5 : IInitable<TD5>, new();

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5, T6, TD6>(TD2 dataT, TD3 dataT1, TD4 dataT2, TD5 dataT3,
            TD6 dataT4)
            where T1 : T, IInitable<T2, T3, T4, T5, T6>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new()
            where T4 : IInitable<TD4>, new()
            where T5 : IInitable<TD5>, new()
            where T6 : IInitable<TD6>, new();

        public void Remove<T1>() where T1 : T;
        public T1 GetFirst<T1>() where T1 : class, T;
        [CanBeNull] public T1 GetFirstNullable<T1>() where T1 : class, T;
        public bool Contains<T1>() where T1 : T;
        [ItemCanBeNull] public List<T1> GetAll<T1>() where T1 : class, T, new();
        public List<List<T>> SplitIntoChunks(int chunkSize);

        public event EventHandler<T> ItemAdded;
    }
}