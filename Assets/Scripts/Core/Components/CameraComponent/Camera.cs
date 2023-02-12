using Wooff.Presentation;

namespace Core.Components.CameraComponent
{
    public class Camera : CoreComponent<CameraData>
    {
        public void SetCamera(IMonoEntity monoEntity)
        {
            var camera = monoEntity.MonoObject.AddComponent<UnityEngine.Camera>();
            camera.backgroundColor = Data.CameraPrefab.backgroundColor;
            camera.clearFlags = Data.CameraPrefab.clearFlags;
        }
    }
}