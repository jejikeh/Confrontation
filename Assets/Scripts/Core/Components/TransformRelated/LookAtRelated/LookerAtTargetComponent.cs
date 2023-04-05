using System;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.TransformRelated.LookAtRelated
{
    [Serializable]
    public class LookerAtTargetComponent : IComponent
    {
        [SerializeField] public float Step;
        [SerializeField] public float MaxOffset;
        [SerializeField] public float MinOffset;
        [SerializeField] public float Speed;
        [SerializeField] public float SmoothTime;
        
        private float _offset;

        public void LookAt(Transform handler, Transform target)
        {
            var position = handler.localPosition;
            var localPosition = position;
            var keepOffset = new Vector3(localPosition.x, _offset,
                localPosition.z);

            keepOffset -= Speed * (_offset - position.y) * Vector3.forward;
            position = Vector3.Lerp(position, keepOffset, Time.deltaTime * SmoothTime);
            handler.localPosition = position;
            handler.LookAt(target);
        }
        
        public void UpdateOffset(float offset, Transform handler)
        {
            if (!(Mathf.Abs(offset) > 0.1f))
                return;

            _offset = handler.localPosition.y + offset * Step;
            _offset = Mathf.Clamp(_offset, MinOffset, MaxOffset);
        }
    }
}