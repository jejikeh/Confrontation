using System;
using Newtonsoft.Json;
using UnityEngine;
using Wooff.Presentation;

namespace Core.Components.MeshComponent
{
    [Serializable]
    public class MeshData
    {
        [JsonIgnore] public UnityEngine.Mesh Mesh;
        [JsonIgnore] public Material Material;
    }

    [Serializable]
    public class MeshDataDictionary : DataDictionary<MeshData>
    {
    }
}