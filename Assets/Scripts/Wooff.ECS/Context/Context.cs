using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Wooff.ECS.Context
{

    public class Context<T> : IContext<T>
    {
        private readonly List<T> _items = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public bool Contains<T1>() where T1 : T
        {
            var item = _items.Any(x => x?.GetType() == typeof(T1));
            return item;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public int Count => _items.Count;
        public bool IsReadOnly => false;

        private List<T> this[Type type]
        {
            get
            {
                var itemList = _items.Where(entity => entity?.GetType() == type);
                if (itemList is null)
                    throw new NullReferenceException($"{type.FullName} item does not exist");

                return itemList.ToList();
            }
        }

        [ItemCanBeNull]
        public List<T1> GetAll<T1>() where T1 : class, T, new()
        {
            var itemList = _items.Where(entity => entity?.GetType() == typeof(T1)).Select(x => x as T1);
            if (itemList is null)
                throw new NullReferenceException($"{typeof(T1).FullName} item does not exist");

            return itemList.ToList();
        }

        public T1 Add<T1>() where T1 : T, new()
        {
            var item = new T1();
            Add(item);
            return item;
        }

        public T1 Add<T1>(Func<T1> action) where T1 : T, IInitable, new()
        {
            var item = IInitable.Initialize<T1>(action);
            Add(item);
            return item;
        }
        
        public T1 Add<T1, T2>(T2 data) where T1 : T, IInitable<T2>, new()
        {
            var item = IInitable<T2>.Initialize<T1>(data);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, T3>(T2 dataT, T3 dataT1) where T1 : T, IInitable<T2, T3>, new()
        {
            var item = IInitable<T2, T3>.Initialize<T1>(dataT, dataT1);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, T3, T4>(T2 dataT, T3 dataT1, T4 dataT2) where T1 : T, IInitable<T2, T3, T4>, new()
        {
            var item = IInitable<T2, T3, T4>.Initialize<T1>(dataT, dataT1, dataT2);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, TD2>(TD2 dataT)
            where T1 : T, IInitable<T2>, new()
            where T2 : IInitable<TD2>, new()
        {
            var argT = new T2();
            argT.Init(dataT);
            var item = IInitable<T2>.Initialize<T1>(argT);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, TD2, T3, TD3>(TD2 dataT, TD3 dataT1)
            where T1 : T, IInitable<T2, T3>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new()
        {
            var argT = new T2();
            argT.Init(dataT);

            var argT1 = new T3();
            argT1.Init(dataT1);

            var item = IInitable<T2, T3>.Initialize<T1>(argT, argT1);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4>(TD2 dataT, TD3 dataT1, TD4 dataT2)
            where T1 : T, IInitable<T2, T3, T4>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new()
            where T4 : IInitable<TD4>, new()
        {
            var argT = new T2();
            argT.Init(dataT);

            var argT1 = new T3();
            argT1.Init(dataT1);

            var argT2 = new T4();
            argT2.Init(dataT2);

            var item = IInitable<T2, T3, T4>.Initialize<T1>(argT, argT1, argT2);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5>(TD2 dataT, TD3 dataT1, TD4 dataT2, TD5 dataT3)
            where T1 : T, IInitable<T2, T3, T4, T5>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new()
            where T4 : IInitable<TD4>, new()
            where T5 : IInitable<TD5>, new()
        {
            var argT = new T2();
            argT.Init(dataT);

            var argT1 = new T3();
            argT1.Init(dataT1);

            var argT2 = new T4();
            argT2.Init(dataT2);

            var argT3 = new T5();
            argT3.Init(dataT3);

            var item = IInitable<T2, T3, T4, T5>.Initialize<T1>(argT, argT1, argT2, argT3);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5, T6, TD6>(TD2 dataT, TD3 dataT1, TD4 dataT2, TD5 dataT3,
            TD6 dataT4)
            where T1 : T, IInitable<T2, T3, T4, T5, T6>, new()
            where T2 : IInitable<TD2>, new()
            where T3 : IInitable<TD3>, new()
            where T4 : IInitable<TD4>, new()
            where T5 : IInitable<TD5>, new()
            where T6 : IInitable<TD6>, new()
        {
            var argT = new T2();
            argT.Init(dataT);

            var argT1 = new T3();
            argT1.Init(dataT1);

            var argT2 = new T4();
            argT2.Init(dataT2);

            var argT3 = new T5();
            argT3.Init(dataT3);

            var argT4 = new T6();
            argT4.Init(dataT4);

            var item = IInitable<T2, T3, T4, T5, T6>.Initialize<T1>(argT, argT1, argT2, argT3, argT4);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, T3, T4, T5>(T2 dataT, T3 dataT1, T4 dataT2, T5 dataT3)
            where T1 : T, IInitable<T2, T3, T4, T5>, new()
        {
            var item = IInitable<T2, T3, T4, T5>.Initialize<T1>(dataT, dataT1, dataT2, dataT3);
            Add(item);
            return item;
        }

        public T1 Add<T1, T2, T3, T4, T5, T6>(T2 dataT, T3 dataT1, T4 dataT2, T5 dataT3, T6 dataT4)
            where T1 : T, IInitable<T2, T3, T4, T5, T6>, new()
        {
            var item = IInitable<T2, T3, T4, T5, T6>.Initialize<T1>(dataT, dataT1, dataT2, dataT3, dataT4);
            Add(item);
            return item;
        }
        
        public void Remove<T1>() where T1 : T
        {
            var itemIndex = _items.FindIndex(temp => temp?.GetType() == typeof(T1));
            if (itemIndex >= 0)
                _items.RemoveAt(itemIndex);
        }

        public T1 GetFirst<T1>() where T1 : class, T
        {
            var found = this[typeof(T1)].FirstOrDefault();

            if (found is null)
                throw new NullReferenceException($"The are no {typeof(T1)} in this {typeof(T).FullName} context");

            if (this[typeof(T1)][0] is not T1 item)
                throw new NullReferenceException(
                    $"{typeof(T1).FullName} class is not relate to {typeof(T).FullName} class");

            return item;
        }

        public T1? GetFirstNullable<T1>() where T1 : class, T
        {
            return this[typeof(T1)].FirstOrDefault() as T1;
        }

        public List<List<T>> SplitIntoChunks(int chunkSize)
        {
            if (chunkSize <= 0)
                throw new ArgumentException("chunkSize must be greater than 0.");

            var retVal = new List<List<T>>();
            var index = 0;
            while (index < _items.Count)
            {
                var count = _items.Count - index > chunkSize ? chunkSize : _items.Count - index;
                retVal.Add(_items.GetRange(index, count));

                index += chunkSize;
            }

            return retVal;
        }

        public event EventHandler<T> ItemAdded;
    }
}