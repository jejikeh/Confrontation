using System.Threading.Tasks;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Component;
using Wooff.ECS.Entity;

namespace Core.Components.HelloWorldComponent
{
    public class SmoothTransform : IComponent<TransformData>
    {
        public string Message { get; set; } = string.Empty;

        public int CompareTo(IComponent other)
        {
            if (other is not HelloWorld helloWorld) 
                return -1;
            
            if (helloWorld == this)
                return 0;

            return -1;
        }

        public IInitable Init()
        {
            return Init(new HelloWorldData()
            {
                Message = "Hello World"
            });
        }

        public IInitable<HelloWorldData> Init(HelloWorldData data)
        {
            Message = data.Message;
            return this;
        }

        public void UpdateOneThread(float timeScale)
        {
            Debug.Log(Message);
        }

        public async Task UpdateParallelAsync(float timeScale)
        {
            Debug.Log(Message);
        }

        public void UpdateOneThread(float timeScale, IEntity data)
        {
            Debug.Log(Message);
        }

        public Task UpdateParallelAsync(float timeScale, IEntity data)
        {
            Debug.Log(Message + data.GetType().FullName);
            return Task.CompletedTask;
        }
    }
}