using System.Linq;
using Core.Components.SmoothLookAtComponent;
using Core.Components.TargetComponent;
using Wooff.ECS.Context;
using Wooff.Presentation;
using Camera = Core.Components.CameraComponent.Camera;

namespace Core.Systems
{
    public class CameraSystem : MonoSystem
    {
        private IMonoEntity _target;
        
        /// <summary>
        /// Find one entity what contains CameraLookAtTarget tag component
        /// </summary>
        /// <param name="data"></param>
        public override void StartOneThread(IContext<IMonoEntity> data)
        {
            base.StartOneThread(data);
            _target = data.FirstOrDefault(x => x.Contains<CameraSmoothLookAtTarget>());
            foreach (var smoothLookAt in data.Where(x => x.Contains<SmoothLookAt>()).Select(x => x.GetFirstNullable<SmoothLookAt>()))
                smoothLookAt?.SetupTarget(_target);
        }
        
        /// <summary>
        /// Setup Camera Component
        /// </summary>
        /// <param name="item"></param>
        protected override void SystemStartOneThread(IMonoEntity item)
        {
            if(!item.Contains<Camera>())
                return;
                
            item.GetFirst<Camera>()?.SetCamera(item);
        }

        protected override void SystemUpdateOneThread(float timeScale, IMonoEntity updateItem)
        {
            if(!updateItem.Contains<Camera>() && !updateItem.Contains<SmoothLookAt>())
                return;
            
            updateItem.GetFirst<SmoothLookAt>().UpdateLookAt(updateItem);
        }
    }
}