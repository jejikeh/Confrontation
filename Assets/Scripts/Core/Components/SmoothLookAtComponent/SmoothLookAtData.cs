using System;
using Wooff.Presentation;

namespace Core.Components.SmoothLookAtComponent
{
    [Serializable]
    public class SmoothLookAtData
    {
        public float Speed;
    }
    
    [Serializable]
    public sealed class SmoothLookAtDataDictionary : DataDictionary<SmoothLookAtData>
    {
    }
}