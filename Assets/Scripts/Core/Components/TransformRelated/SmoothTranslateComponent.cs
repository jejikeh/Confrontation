using System;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.TransformRelated
{
    [Serializable]
    public class SmoothTranslateComponent : IComponent
    {
        [SerializeField] public float SmoothMovementTime;
        [SerializeField] public float SpeedMovement;

        private Vector3 _lastPosition;
        private Vector3 _velocity;
        private Vector3 _direction;
        private float _speed;

        public Vector3 GetHandlerRight(Transform wrapperComponent)
        {
            var right = wrapperComponent.right;
            right.y = 0;
            return right;
        }

        public Vector3 GetHandlerForward(Transform wrapperComponent)
        {
            var forward = wrapperComponent.forward;
            forward.y = 0;
            return forward;
        }

        public Vector3 GetHandlerUp(Transform wrapperComponent)
        {
            return wrapperComponent.up;
        }
        
        public SmoothTranslateComponent(Transform transformWrapperComponent)
        {
            _lastPosition = transformWrapperComponent.position;
        }

        public void UpdateVelocity(float timeScale, Transform handler)
        {
            var position = handler.transform.position;
            _velocity = (position - _lastPosition) * timeScale / Time.deltaTime;
            _lastPosition = position;
        }

        public void SetMovementDirection(Vector3 direction, Transform handler)
        {
            var inputValue = 
                direction.x * GetHandlerRight(handler) +
                direction.y * GetHandlerForward(handler) + 
                direction.z * GetHandlerUp(handler);
            
            if (direction.sqrMagnitude > 0.1f)
                _direction += inputValue;
        }
        
        public void SetPosition(Vector3 position, Transform handler)
        {
            _direction += position - handler.transform.position;
        }
        
        public void UpdatePosition(float timeScale, Transform handler)
        {
            if (_direction.sqrMagnitude > 0.1f)
            {
                _speed = Mathf.Lerp(
                    _speed, 
                    SpeedMovement,
                    Time.deltaTime * SmoothMovementTime * timeScale);
                handler.transform.position += _direction * (_speed * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.Lerp(
                    _velocity, 
                    Vector3.zero,
                    Time.deltaTime * SmoothMovementTime * timeScale);
                if (_velocity.magnitude > 0.01f)
                    handler.transform.position += _velocity * Time.deltaTime;
            }

            _direction = Vector3.zero;
        }
    }
}