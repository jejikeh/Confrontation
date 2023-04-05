using System;
using UnityEngine;

namespace Core.Components.TransformRelated
{
    [Serializable]
    public class SmoothRotateComponent : Wooff.ECS.Components.IComponent
    {
        [SerializeField] public float RotationSpeed;
        [SerializeField] public float RotationTime;
        [HideInInspector] public Quaternion NewRotation;

        public SmoothRotateComponent(Transform transformWrapperComponent)
        {
            NewRotation = transformWrapperComponent.rotation;
        }
    }
}   