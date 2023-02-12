using Core.Components.HelloWorldComponent;
using Core.Components.MeshComponent;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Entity;
using Wooff.Presentation;
using Mesh = Core.Components.MeshComponent.Mesh;

namespace Core.Entities
{
    public class Bob : Entity<IMonoComponent>, IMonoEntity
    {
        public Bob()
        {
            Add<HelloWorld, HelloWorldData>(DataGlobalStorage.HelloWorldDataStatic);
            Add<Mesh, MeshData>(DataGlobalStorage.MeshDataStatic);
        }

        public GameObject MonoObject { get; set; }
    }
}