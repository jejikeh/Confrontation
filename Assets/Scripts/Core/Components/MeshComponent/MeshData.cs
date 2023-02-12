using System;
using UnityEngine;
using Wooff.Presentation;

namespace Core.Components.MeshComponent
{
    [System.Serializable]
    public class MeshData
    {
        public UnityEngine.Mesh Mesh;
        public Material Material;
    }

    [Serializable]
    public class MeshDataDictionary : DataDictionary<MeshData>
    {
    }
}