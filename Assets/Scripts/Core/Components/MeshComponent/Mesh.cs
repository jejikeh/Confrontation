using UnityEngine;
using Wooff.Presentation;

namespace Core.Components.MeshComponent
{
    public class Mesh : CoreComponent<MeshData>
    {
        public void SetMesh(IMonoEntity monoEntity)
        {
            monoEntity.MonoObject.AddComponent<MeshRenderer>();
            var meshFilter = monoEntity.MonoObject.AddComponent<MeshFilter>();
            meshFilter.mesh = Data.Mesh;
        }
    }
}