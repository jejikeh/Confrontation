using Core.Components.CameraComponent;
using Core.Components.HelloWorldComponent;
using Core.Components.MeshComponent;
using UnityEngine;
using Wooff.ECS.Entity;
using Wooff.Presentation;
using Camera = Core.Components.CameraComponent.Camera;

namespace Core.Entities
{
    public class MainCamera : Entity<IMonoComponent>, IMonoEntity
    {
        public MainCamera()
        {
            Add<HelloWorld, HelloWorldData>(DataStorage.GetHelloWorldData("Camera"));
            Add<Camera, CameraData>(DataStorage.GetCameraData("Camera"));
        }
        
        public GameObject MonoObject { get; set; }
    }
}