using UnityEngine;
using Wooff.Presentation;

namespace Core.Components.MeshComponent
{
    public class Mesh : MonoComponent<MeshData>
    {
        public void SetMesh(IMonoEntity monoEntity)
        {
            var meshRenderer = monoEntity.MonoObject.AddComponent<MeshRenderer>();
            var meshFilter = monoEntity.MonoObject.AddComponent<MeshFilter>();
            meshRenderer.material = Data.Material;
            meshFilter.mesh = Data.Mesh;
        }
    }
}