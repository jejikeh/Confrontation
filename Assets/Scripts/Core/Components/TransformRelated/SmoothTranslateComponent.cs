using System;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.SmoothTranslateComponent
{
    [Serializable]
    public class SmoothTranslate : IComponent
    {
        [SerializeField] public float SmoothMovementTime;
        [SerializeField] public float SpeedMovement;

        private Vector3 _lastPosition;
        private Vector3 _velocity;
        private float _speed;
        private Vector3 _direction;

        public Vector3 GetHandlerRight(TransformWrapper wrapper)
        {
            var right = wrapper.Transform.right;
            right.y = 0;
            return right;
        }

        public Vector3 GetHandlerForward(TransformWrapper wrapper)
        {
            var forward = wrapper.Transform.forward;
            forward.y = 0;
            return forward;
        }

        public Vector3 GetHandlerUp(TransformWrapper wrapper)
        {
            return wrapper.Transform.up;
        }
        
        public SmoothTranslate(TransformWrapper transformWrapper)
        {
            _lastPosition = transformWrapper.Transform.position;
        }

        public void UpdateVelocity(float timeScale, TransformWrapper handler)
        {
            var position = handler.Transform.transform.position;
            _velocity = (position - _lastPosition) * timeScale / Time.deltaTime;
            _lastPosition = position;
        }

        public void SetMovementDirection(Vector3 direction, TransformWrapper handler)
        {
            var inputValue = 
                direction.x * GetHandlerRight(handler) +
                direction.y * GetHandlerForward(handler) + 
                direction.z * GetHandlerUp(handler);
            
            if (direction.sqrMagnitude > 0.1f)
                _direction += inputValue;
        }
        
        public void SetPosition(Vector3 position, TransformWrapper handler)
        {
            _direction += position - handler.Transform.transform.position;
        }
        
        public void UpdatePosition(float timeScale, TransformWrapper handler)
        {
            if (_direction.sqrMagnitude > 0.1f)
            {
                _speed = Mathf.Lerp(
                    _speed, 
                    SpeedMovement,
                    Time.deltaTime * SmoothMovementTime * timeScale);
                handler.Transform.transform.position += _direction * (_speed * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.Lerp(
                    _velocity, 
                    Vector3.zero,
                    Time.deltaTime * SmoothMovementTime * timeScale);
                if (_velocity.magnitude > 0.01f)
                    handler.Transform.transform.position += _velocity * Time.deltaTime;
            }

            _direction = Vector3.zero;
        }   
    }
}