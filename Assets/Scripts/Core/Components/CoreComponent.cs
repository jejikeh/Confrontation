using System.Collections.Generic;
using System.Threading.Tasks;
using Wooff.ECS;
using Wooff.ECS.Component;
using Wooff.Presentation;

namespace Core.Components
{
    public class CoreComponent<T> : IMonoComponent<T> where T : new()
    {
        protected T Data { get; set; }

        public IInitable Init()
        {
            return Init(new T());
        }

        public int CompareTo(IComponent other)
        {
            if (other is not { } component) 
                return -1;
        
            if (component == this)
                return 0;

            return -1;
        }

        public virtual void UpdateOneThread(float timeScale)
        {
        
        }

        public Task UpdateParallelAsync(float timeScale)
        {
            return Task.WhenAll();
        }

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
