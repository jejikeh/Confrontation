using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.SmoothLookAtTargetComponent
{
    public class SmoothLookAtTarget : Component<SmoothLookAtTargetConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        private float _offset;

        public SmoothLookAtTarget(SmoothLookAtTargetConfig data, IMonoEntity handler) : base(data, handler)
        {
            _offset = Config.MaxOffset;
        }

        public void LookAt(float timeScale, IMonoEntity target)
        {
            var position = Handler.MonoObject.transform.localPosition;
            var localPosition = position;
            var keepOffset = new Vector3(localPosition.x, _offset,
                localPosition.z);

            keepOffset -= Config.Speed * (_offset - position.y) * Vector3.forward;
            position = Vector3.Lerp(position, keepOffset, Time.deltaTime * Config.SmoothTime * timeScale);
            Handler.MonoObject.transform.localPosition = position;
            Handler.MonoObject.transform.LookAt(target.MonoObject.transform);
        }
        
        public void UpdateOffset(float offset)
        {
            if (!(Mathf.Abs(offset) > 0.1f))
                return;

            _offset = Handler.MonoObject.transform.localPosition.y + offset * Config.Step;
            _offset = Mathf.Clamp(_offset, Config.MinOffset, Config.MaxOffset);
        }

        IConfig IConfigurable<IConfig>.Config => Config;
    }
}