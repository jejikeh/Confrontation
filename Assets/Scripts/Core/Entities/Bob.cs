using Core.Components.HelloWorldComponent;
using Core.Components.MeshComponent;
using Newtonsoft.Json;
using UnityEngine;
using Wooff.ECS.Entity;
using Wooff.Presentation;
using Camera = Core.Components.CameraComponent.Camera;

namespace Core.Entities
{
    public class Bob : Entity<IMonoComponent>, IMonoEntity
    {
        public Bob()
        {
            Add<HelloWorld, HelloWorldData>(DataStorage.GetHelloWorldData("Bob"));
            Add<Components.MeshComponent.Mesh, MeshData>(DataStorage.GetMeshData("Bob"));
        }

        public GameObject MonoObject { get; set; }
    }
}