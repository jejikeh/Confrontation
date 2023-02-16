using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.SmoothTranslateComponent
{
    public class SmoothTranslate : Component<SmoothTranslateConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        private Vector3 _lastPosition;
        private Vector3 _velocity;
        private float _speed;
        private Vector3 _direction;
        
        public SmoothTranslate(SmoothTranslateConfig data, IMonoEntity handler) : base(data, handler)
        {
            _lastPosition = Handler.MonoObject.transform.position;
        }

        public void UpdateVelocity(float timeScale)
        {
            var position = Handler.MonoObject.transform.position;
            _velocity = (position - _lastPosition) * timeScale / Time.deltaTime;
            _lastPosition = position;
        }

        public void SetMovementDirection(Vector3 direction)
        {
            var inputValue = 
                direction.x * Config.GetHandlerRight(Handler) +
                direction.y * Config.GetHandlerForward(Handler) + 
                direction.z * Config.GetHandlerUp(Handler);
            
            if (direction.sqrMagnitude > 0.1f)
                _direction += inputValue;
        }
        
        public void UpdatePosition(float timeScale)
        {
            if (_direction.sqrMagnitude > 0.1f)
            {
                _speed = Mathf.Lerp(_speed, Config.SpeedMovement,
                    Time.deltaTime * Config.SmoothMovementTime * timeScale);
                Handler.MonoObject.transform.position += _direction * (_speed * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.Lerp(_velocity, Vector3.zero,
                    Time.deltaTime * Config.SmoothMovementTime * timeScale);
                if (_velocity.magnitude > 0.01f)
                    Handler.MonoObject.transform.position += _velocity * Time.deltaTime;
            }

            _direction = Vector3.zero;
        }
        

        IConfig IConfigurable<IConfig>.Config => Config;
    }
}