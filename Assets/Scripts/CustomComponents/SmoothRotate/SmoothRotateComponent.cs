using Core;
using UnityEngine;

namespace CustomComponents.SmoothRotate
{
    public class SmoothRotateComponent : CustomComponent<SmoothRotateComponentConfig>
    {
        private Quaternion _newRotation;
        
        public SmoothRotateComponent(SmoothRotateComponentConfig customComponentConfig) : base(customComponentConfig)
        {
            _newRotation = ComponentConfig.Handler.rotation;
        }

        protected override void OnUpdate(float timeScale)
        {
            ComponentConfig.Handler.rotation = Quaternion.Lerp(ComponentConfig.Handler.rotation, _newRotation, Time.deltaTime * timeScale * ComponentConfig.RotationTime);
        }

        public void Rotate(Vector3 direction)
        {
            _newRotation = Quaternion.Euler(direction * ComponentConfig.RotationSpeed + ComponentConfig.Handler.rotation.eulerAngles);
        }
    }
}   