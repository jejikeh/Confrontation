using System.Collections.Generic;
using System.Threading.Tasks;
using Wooff.ECS;
using Wooff.ECS.Component;
using Wooff.Presentation;

namespace Core.Components
{
    public class CoreComponent : IMonoComponent
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
    public class CoreComponent<T> : CoreComponent, IMonoComponent<T> where T : new()
    {
        public T Data { get; set; }
        
        public List<Task> UpdateParallelToMainThread(float timeScale)
        {
            return new List<Task>();
        }

        public IInitable<T> Init(T data)
        {
            Data = data;
            return this;
        }
    }
}
