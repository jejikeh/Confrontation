using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components.CameraComponent;
using Core.Components.MeshComponent;
using Core.Components.SmoothLookAtComponent;
using UnityEngine;

namespace Wooff.Presentation
{
    [Serializable]
    public class DataStorage : Singleton<DataStorage>
    {
        [SerializeField] public List<MeshDataDictionary> MeshDatas;
        public static MeshData GetMeshData(string key) => Instance.MeshDatas.FirstOrDefault(x => x .Key == key)?.Value;
        
        public List<CameraDataDictionary> CameraDatas;
        public static CameraData GetCameraData(string key) => Instance.CameraDatas.FirstOrDefault(x => x.Key == key)?.Value;
        
        public List<SmoothLookAtDataDictionary> SmoothLookAtDataDictionaries;
        public static SmoothLookAtData GetSmoothLookAtData(string key) => Instance.SmoothLookAtDataDictionaries.FirstOrDefault(x => x.Key == key)?.Value;
    }

    public class DataDictionary<T>
    {
        public string Key;
        public T Value;
    }
}