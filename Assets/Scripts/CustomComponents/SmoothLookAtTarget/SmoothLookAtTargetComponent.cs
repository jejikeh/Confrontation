using Core;
using UnityEngine;

namespace CustomComponents.SmoothLookAtTarget
{
    public class SmoothLookAtTargetComponent : CustomComponent<SmoothLookAtTargetComponentConfig>
    {
        private float _offset;
        
        public SmoothLookAtTargetComponent(SmoothLookAtTargetComponentConfig customComponentConfig) : base(customComponentConfig)
        {
            _offset = ComponentConfig.MaxOffset;
            ComponentConfig.Handler.LookAt(ComponentConfig.Target);
        }

        protected override void OnUpdate(float timeScale)
        {
            var position = ComponentConfig.Handler.localPosition;
            var localPosition = position;
            var keepOffset = new Vector3(localPosition.x, _offset,
                localPosition.z);

            keepOffset -= ComponentConfig.Speed * (_offset - position.y) * Vector3.forward;
            position = Vector3.Lerp(position, keepOffset, Time.deltaTime * ComponentConfig.SmoothTime * timeScale);
            ComponentConfig.Handler.localPosition = position;
            ComponentConfig.Handler.LookAt(ComponentConfig.Target);
        }

        public void UpdateOffset(float offset)
        {
            if (!(Mathf.Abs(offset) > 0.1f))
                return;

            _offset = ComponentConfig.Handler.localPosition.y + offset * ComponentConfig.Step;
            _offset = Mathf.Clamp(_offset, ComponentConfig.MinOffset, ComponentConfig.MaxOffset);
        }
    }
}