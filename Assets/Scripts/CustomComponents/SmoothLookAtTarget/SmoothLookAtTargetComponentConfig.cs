using System;
using Core.Interfaces;
using UnityEngine;

namespace CustomComponents.SmoothLookAtTarget
{
    [Serializable]
    public class SmoothLookAtTargetComponentConfig : ICustomComponentConfig
    {
        public float Step;
        public float MaxOffset;
        public float MinOffset;
        public float Speed;
        public float SmoothTime;
        public Transform Handler;
        public Transform Target;
    }
}