using Core.Components.CameraComponent;
using Core.Components.SmoothLookAtComponent;
using UnityEngine;
using Wooff.ECS.Entity;
using Wooff.Presentation;
using Camera = Core.Components.CameraComponent.Camera;

namespace Core.Entities
{
    public class MainCamera : Entity<IMonoComponent>, IMonoEntity
    {
        public MainCamera()
        {
            Add<Camera, CameraData>(DataStorage.GetCameraData("MainCamera"));
            Add<SmoothLookAt, SmoothLookAtData>(DataStorage.GetSmoothLookAtData("MainCamera"));
        }
        
        public GameObject MonoObject { get; set; }
    }
}