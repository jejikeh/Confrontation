using System.Threading.Tasks;
using Core.Components.HelloWorldComponent;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Component;
using Wooff.ECS.Entity;

namespace Core.Components.TransformComponent
{
    public class SmoothTransform : IComponent<TransformData>
    {
        
        public float Speed { get; set; }

        public int CompareTo(IComponent other)
        {
            if (other is not SmoothTransform helloWorld) 
                return -1;
            
            if (helloWorld == this)
                return 0;

            return -1;
        }


        public IInitable<TransformData> Init(TransformData data)
        {
            Speed = data.Speed;
            return this;
        }

        public IInitable Init()
        {
            return this;
        }
    }
}