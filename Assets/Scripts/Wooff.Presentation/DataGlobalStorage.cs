using System;
using Core.Components.HelloWorldComponent;
using Core.Components.MeshComponent;
using Core.Entities;
using UnityEngine;

namespace Wooff.Presentation
{
    [System.Serializable]
    public class DataGlobalStorage : Singleton<DataGlobalStorage>
    {
        public HelloWorldData HelloWorldData;
        public static HelloWorldData HelloWorldDataStatic => Instance.HelloWorldData;

        public MeshData MeshData;
        public static MeshData MeshDataStatic => Instance.MeshData;
    }
}