using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.SmoothRotateComponent
{
    public class SmoothRotate : Component<SmoothRotateConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        private Quaternion _newRotation;

        public SmoothRotate(SmoothRotateConfig data, IMonoEntity handler) : base(data, handler)
        {
            _newRotation = handler.MonoObject.transform.rotation;
        }
        
        public void Update(float timeScale)
        {
            Handler.MonoObject.transform.rotation = Quaternion.Lerp(Handler.MonoObject.transform.rotation, _newRotation, Time.deltaTime * timeScale * Config.RotationTime);
        }

        public void Rotate(Vector3 direction)
        {
            _newRotation = Quaternion.Euler(direction * Config.RotationSpeed + Handler.MonoObject.transform.rotation.eulerAngles);
        }

        IConfig IConfigurable<IConfig>.Config => Config;
    }
}