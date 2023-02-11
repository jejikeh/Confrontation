using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Entity;

namespace Wooff.Presentation
{
    public abstract class MonoEntity<T> : MonoBehaviour, IMonoEntity where T : IEntity<IMonoComponent>, new()
    {
        private T _monoEntityImplementation = new ();
        
        public IEnumerator<IMonoComponent> GetEnumerator()
        {
            return _monoEntityImplementation.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_monoEntityImplementation).GetEnumerator();
        }

        public void Add(IMonoComponent item)
        {
            _monoEntityImplementation.Add(item);
        }

        public void Clear()
        {
            _monoEntityImplementation.Clear();
        }

        public bool Contains(IMonoComponent item)
        {
            return _monoEntityImplementation.Contains(item);
        }

        public void CopyTo(IMonoComponent[] array, int arrayIndex)
        {
            _monoEntityImplementation.CopyTo(array, arrayIndex);
        }

        public bool Remove(IMonoComponent item)
        {
            return _monoEntityImplementation.Remove(item);
        }

        public int Count => _monoEntityImplementation.Count;

        public bool IsReadOnly => _monoEntityImplementation.IsReadOnly;

        public T1 Add<T1>() where T1 : IMonoComponent, new()
        {
            return _monoEntityImplementation.Add<T1>();
        }

        public T1 Add<T1>(Func<T1> action) where T1 : IMonoComponent, IInitable, new()
        {
            return _monoEntityImplementation.Add(action);
        }

        public T1 Add<T1, T2>(T2 data) where T1 : IMonoComponent, IInitable<T2>, new()
        {
            return _monoEntityImplementation.Add<T1, T2>(data);
        }

        public T1 Add<T1, T2, T3>(T2 dataT, T3 dataT1) where T1 : IMonoComponent, IInitable<T2, T3>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, T3>(dataT, dataT1);
        }

        public T1 Add<T1, T2, T3, T4>(T2 dataT, T3 dataT1, T4 dataT2) where T1 : IMonoComponent, IInitable<T2, T3, T4>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, T3, T4>(dataT, dataT1, dataT2);
        }

        public T1 Add<T1, T2, T3, T4, T5>(T2 dataT, T3 dataT1, T4 dataT2, T5 dataT3) where T1 : IMonoComponent, IInitable<T2, T3, T4, T5>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, T3, T4, T5>(dataT, dataT1, dataT2, dataT3);
        }

        public T1 Add<T1, T2, T3, T4, T5, T6>(T2 dataT, T3 dataT1, T4 dataT2, T5 dataT3, T6 dataT4) where T1 : IMonoComponent, IInitable<T2, T3, T4, T5, T6>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, T3, T4, T5, T6>(dataT, dataT1, dataT2, dataT3, dataT4);
        }

        public T1 Add<T1, T2, TD2>(TD2 dataT) where T1 : IMonoComponent, IInitable<T2>, new() where T2 : IInitable<TD2>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, TD2>(dataT);
        }

        public T1 Add<T1, T2, TD2, T3, TD3>(TD2 dataT, TD3 dataT1) where T1 : IMonoComponent, IInitable<T2, T3>, new() where T2 : IInitable<TD2>, new() where T3 : IInitable<TD3>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, TD2, T3, TD3>(dataT, dataT1);
        }

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4>(TD2 dataT, TD3 dataT1, TD4 dataT2) where T1 : IMonoComponent, IInitable<T2, T3, T4>, new() where T2 : IInitable<TD2>, new() where T3 : IInitable<TD3>, new() where T4 : IInitable<TD4>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, TD2, T3, TD3, T4, TD4>(dataT, dataT1, dataT2);
        }

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5>(TD2 dataT, TD3 dataT1, TD4 dataT2, TD5 dataT3) where T1 : IMonoComponent, IInitable<T2, T3, T4, T5>, new() where T2 : IInitable<TD2>, new() where T3 : IInitable<TD3>, new() where T4 : IInitable<TD4>, new() where T5 : IInitable<TD5>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5>(dataT, dataT1, dataT2, dataT3);
        }

        public T1 Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5, T6, TD6>(TD2 dataT, TD3 dataT1, TD4 dataT2, TD5 dataT3, TD6 dataT4) where T1 : IMonoComponent, IInitable<T2, T3, T4, T5, T6>, new() where T2 : IInitable<TD2>, new() where T3 : IInitable<TD3>, new() where T4 : IInitable<TD4>, new() where T5 : IInitable<TD5>, new() where T6 : IInitable<TD6>, new()
        {
            return _monoEntityImplementation.Add<T1, T2, TD2, T3, TD3, T4, TD4, T5, TD5, T6, TD6>(dataT, dataT1, dataT2, dataT3, dataT4);
        }

        public void Remove<T1>() where T1 : IMonoComponent
        {
            _monoEntityImplementation.Remove<T1>();
        }

        public T1 GetFirst<T1>() where T1 : class, IMonoComponent
        {
            return _monoEntityImplementation.GetFirst<T1>();
        }

        public T1 GetFirstNullable<T1>() where T1 : class, IMonoComponent
        {
            return _monoEntityImplementation.GetFirstNullable<T1>();
        }

        public bool Contains<T1>() where T1 : IMonoComponent
        {
            return _monoEntityImplementation.Contains<T1>();
        }

        public List<T1> GetAll<T1>() where T1 : class, IMonoComponent, new()
        {
            return _monoEntityImplementation.GetAll<T1>();
        }

        public List<List<IMonoComponent>> SplitIntoChunks(int chunkSize)
        {
            return _monoEntityImplementation.SplitIntoChunks(chunkSize);
        }

        public event EventHandler<IMonoComponent> ItemAdded
        {
            add => _monoEntityImplementation.ItemAdded += value;
            remove => _monoEntityImplementation.ItemAdded -= value;
        }
        
        public abstract IInitable Init();
        
        public void UpdateOneThread(float timeScale)
        {
            foreach (var monoComponent in this)
                monoComponent.UpdateOneThread(timeScale);
        }

        public Task UpdateParallelAsync(float timeScale)
        {
            Parallel.ForEach(SplitIntoChunks(100), async chunk =>
            {
                foreach (var monoComponent in chunk)
                    await monoComponent.UpdateParallelAsync(timeScale);
            });

            return Task.WhenAll();
        }

        public virtual List<Task> UpdateParallelToMainThread(float timeScale)
        {
            return new List<Task>();
        }

        public virtual GameObject MonoObject { get; set; }
    }
}