using UnityEngine;
using Wooff.Presentation;

namespace Core.Components.SmoothLookAtComponent
{
    public class SmoothLookAt : MonoComponent<SmoothLookAtData>
    {
        private Transform _targetTransform;
        private Vector3 _direction;
        private Quaternion _rotationGoad;

        public void SetupTarget(IMonoEntity target)
        {
            _targetTransform = target.MonoObject.transform;
        }
        
        public void UpdateLookAt(IMonoEntity handler)
        {
            _direction = (_targetTransform.position - handler.MonoObject.transform.position).normalized;
            _rotationGoad = Quaternion.LookRotation(_direction);
            handler.MonoObject.transform.rotation = Quaternion.Slerp(handler.MonoObject.transform.rotation, _rotationGoad, Data.Speed);
        }
    }
}
