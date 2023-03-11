using System;
using UnityEngine;

namespace Core.Components.SmoothRotateComponent
{
    [Serializable]
    public class SmoothRotate : Wooff.ECS.Components.IComponent
    {
        [SerializeField] public float RotationSpeed;
        [SerializeField] public float RotationTime;
        [HideInInspector] public Quaternion NewRotation;

        public SmoothRotate(TransformWrapper transformWrapper)
        {
            NewRotation = transformWrapper.Transform.rotation;
        }

        public void Rotate(Vector3 direction, TransformWrapper transformWrapper)
        {
            NewRotation = Quaternion.Euler(
                direction * RotationSpeed + 
                transformWrapper.Transform.rotation.eulerAngles);
        }
    }
}   