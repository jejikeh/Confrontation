using System;
using UnityEngine;
using Wooff.Presentation;

namespace Core.Components.CameraComponent
{
    [Serializable]
    public class CameraData
    {
        public Color BackgroundColor;
        public CameraClearFlags CameraClearFlags;
    }
    
    [Serializable]
    public sealed class CameraDataDictionary : DataDictionary<CameraData>
    {
    }
}