using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components.CameraComponent;
using Core.Components.HelloWorldComponent;
using Core.Components.MeshComponent;
using UnityEditor;
using UnityEngine;
using HelloWorldDictionary = Core.Components.HelloWorldComponent.HelloWorldDictionary;
using Mesh = UnityEngine.Mesh;

namespace Wooff.Presentation
{
    [Serializable]
    public class DataStorage : Singleton<DataStorage>
    {
        [SerializeField] public List<MeshDataDictionary> MeshDatas;
        public static MeshData GetMeshData(string key) => Instance.MeshDatas.FirstOrDefault(x => x .Key == key)?.Value;
        
        public List<HelloWorldDictionary> HelloWorldDatas;
        public static HelloWorldData GetHelloWorldData(string key) => Instance.HelloWorldDatas.FirstOrDefault(x => x.Key == key)?.Value;
        
        public List<CameraDataDictionary> CameraDatas;
        public static CameraData GetCameraData(string key) => Instance.CameraDatas.FirstOrDefault(x => x.Key == key)?.Value;
    }

    public class DataDictionary<T>
    {
        public string Key;
        public T Value;
    }
}