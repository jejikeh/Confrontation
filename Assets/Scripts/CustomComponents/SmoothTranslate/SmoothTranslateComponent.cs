using Core;
using Unity.VisualScripting;
using UnityEngine;

namespace CustomComponents.SmoothTranslate
{
    /// <summary>
    /// Smoothly moves local transform or global transform to the desired vector
    /// </summary>
    public class SmoothTranslateComponent : CustomComponent<SmoothTranslateComponentConfig>
    {
        private Vector3 _lastPosition;
        private Vector3 _velocity;
        private Vector3 _direction;
        private float _speed;
        private readonly Transform _handler;

        public SmoothTranslateComponent(SmoothTranslateComponentConfig customComponentConfig) : base(customComponentConfig)
        {
            _lastPosition = ComponentConfig.Handler.position;
            _handler = ComponentConfig.Handler;
        }
        
        protected override void OnUpdate(float timeScale)
        {
            UpdateVelocity();
            UpdatePosition();
        }

        private void UpdateVelocity()
        {
            var position = _handler.position;
            _velocity = (position - _lastPosition) / Time.deltaTime;
            _lastPosition = position;
        }
        
        public void SetRelativeMovementDirection(Vector3 direction)
        {
            var inputValue = direction.x * ComponentConfig.GetHandlerRight() +
                             direction.y * ComponentConfig.GetHandlerForward() + 
                             direction.z * ComponentConfig.GetHandlerUp();
            if (inputValue.sqrMagnitude > 0.1f)
                _direction += inputValue;
        }
        
        public void SetMovementDirection(Vector3 direction)
        {
            if (direction.sqrMagnitude > 0.1f)
                _direction += direction;
        }

        public void SetPosition(Vector3 position)
        {
            _direction += position - _handler.position;
        }

        private void UpdatePosition()
        {
            if (_direction.sqrMagnitude > 0.1f)
            {
                _speed = Mathf.Lerp(_speed, ComponentConfig.SpeedMovement,
                    Time.deltaTime * ComponentConfig.SmoothMovementTime);
                _handler.position += _direction * (_speed * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.Lerp(_velocity, Vector3.zero,
                    Time.deltaTime * ComponentConfig.SmoothMovementTime);
                if (_velocity.magnitude > 0.01f)
                    _handler.position += _velocity * Time.deltaTime;
            }

            _direction = Vector3.zero;
        }
    }
}