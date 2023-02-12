using UnityEngine;
using Wooff.Presentation;

namespace Core.Components.MeshComponent
{
    public class Mesh : CoreComponent<MeshData>
    {
        public void SetMesh(IMonoEntity monoEntity)
        {
            var meshRenderer = monoEntity.MonoObject.AddComponent<MeshRenderer>();
            meshRenderer.material = Data.Material;
            var meshFilter = monoEntity.MonoObject.AddComponent<MeshFilter>();
            meshFilter.mesh = Data.Mesh;
        }
    }
}