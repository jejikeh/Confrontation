using System;
using Wooff.Presentation;

namespace Core.Components.CameraComponent
{
    [Serializable]
    public class CameraData
    {
        public UnityEngine.Camera CameraPrefab;
    }
    
    [Serializable]
    public sealed class CameraDataDictionary : DataDictionary<CameraData>
    {
    }
}