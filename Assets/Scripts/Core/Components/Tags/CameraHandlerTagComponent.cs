using System;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags
{
    public class CameraHandlerTagComponent : IComponent
    {
        public UnityGameObjectComponent UnityGameObjectComponent;
        public SmoothRotateComponent SmoothRotateComponent;
        public SmoothTranslateComponent SmoothTranslateComponent;
        public UnityCameraComponent UnityCameraComponent;
        public UserControllableTagComponent UserControllableTagComponent;
        
        public CameraHandlerTagComponent(CameraHandlerTagComponentData cameraHandlerTagComponentData)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(cameraHandlerTagComponentData.UnityGameObjectComponent);
            SmoothRotateComponent = cameraHandlerTagComponentData.SmoothRotateComponent;
            SmoothTranslateComponent = cameraHandlerTagComponentData.SmoothTranslateComponent;
            UnityCameraComponent = new UnityCameraComponent();
            UserControllableTagComponent = new UserControllableTagComponent();
        }

        public IEntity CreateCameraEntityContainer()
        {
            return new Entity(
                UnityGameObjectComponent,
                SmoothRotateComponent,
                SmoothTranslateComponent,
                UnityCameraComponent,
                UserControllableTagComponent,
                this);
        }
    }

    [Serializable]
    public class CameraHandlerTagComponentData
    {
        [Header("Camera Handler Tag Component")]
        [SerializeField] public UnityGameObjectComponent UnityGameObjectComponent;
        [SerializeField] public SmoothRotateComponent SmoothRotateComponent;
        [SerializeField] public SmoothTranslateComponent SmoothTranslateComponent;
    }
}