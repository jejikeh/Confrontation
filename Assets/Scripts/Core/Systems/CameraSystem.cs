using System.Threading.Tasks;
using UnityEngine;
using Wooff.Presentation;
using Camera = Core.Components.CameraComponent.Camera;
using Mesh = Core.Components.MeshComponent.Mesh;

namespace Core.Systems
{
    public class CameraSystem : MonoSystem
    {
        // TODO: Replace this by IMonoComponent IStartable
        protected override void SystemStartOneThread(IMonoEntity item)
        {
            if(!item.Contains<Camera>())
                return;
                
            item.GetFirst<Camera>()?.SetCamera(item);
        }
    }
}