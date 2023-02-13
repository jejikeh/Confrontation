using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wooff.ECS;
using Wooff.Presentation;

namespace Core.Components
{
    public class MonoComponent : IMonoComponent
    {
        public IInitable Init()
        {
            return this;
        }

        public virtual void UpdateOneThread(float timeScale)
        {
        }

        public Task UpdateParallelAsync(float timeScale)
        {
            return Task.WhenAll();
        }
    }
    public class MonoComponent<T> : MonoComponent, IMonoComponent<T> where T : new()
    {
        public T Data { get; set; }
        
        public IInitable<T> Init(T data)
        {
            Data = data;
            return this;
        }
    }
}
