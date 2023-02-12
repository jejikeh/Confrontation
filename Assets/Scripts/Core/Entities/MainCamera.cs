using Core.Components.HelloWorldComponent;
using Core.Components.MeshComponent;
using UnityEngine;
using Wooff.ECS.Entity;
using Wooff.Presentation;

namespace Core.Entities
{
    public class Camera : Entity<IMonoComponent>, IMonoEntity
    {
        public Camera()
        {
            Add<HelloWorld, HelloWorldData>(DataStorage.GetHelloWorldData("Camera"));
        }
        
        public GameObject MonoObject { get; set; }
    }
}