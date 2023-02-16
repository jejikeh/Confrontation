using System;
using Wooff.ECS;

namespace Core.Components.SmoothRotateComponent
{
    [Serializable]
    public class SmoothRotateConfig : IConfig
    {
        public float RotationSpeed;
        public float RotationTime;
    }
}