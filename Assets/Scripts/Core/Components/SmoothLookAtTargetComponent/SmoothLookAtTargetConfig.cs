using System;
using Wooff.ECS;

namespace Core.Components.SmoothLookAtTargetComponent
{
    [Serializable]
    public class SmoothLookAtTargetConfig : IConfig
    {
        public float Step;
        public float MaxOffset;
        public float MinOffset;
        public float Speed;
        public float SmoothTime;
    }
}