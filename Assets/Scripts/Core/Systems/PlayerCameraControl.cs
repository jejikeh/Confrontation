using System.Linq;
using Core.Components;
using Core.Components.Tags;
using Core.Components.Tags.UiTags;
using UnityEngine;
using Wooff.ECS.Contexts;

namespace Core.Systems
{
    public class PlayerCameraControl : Wooff.ECS.Systems.System
    {
        private CameraHandlerTagComponent _cameraMovePoint;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _cameraMovePoint = context.ContextGetAllFromMap(typeof(CameraHandlerTagComponent)).FirstOrDefault().ContextGet<CameraHandlerTagComponent>();
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            var smoothRotate = _cameraMovePoint.SmoothRotateComponent;
            var unityObjectComponent = _cameraMovePoint.UnityGameObjectComponent;
            var smoothTranslate = _cameraMovePoint.SmoothTranslateComponent;

            if (Input.GetKey(KeyCode.Q))
                smoothRotate.NewRotation = Quaternion.Euler(Vector3.down * smoothRotate.RotationSpeed + unityObjectComponent.UnitySceneObject.transform.rotation.eulerAngles);
            if (Input.GetKey(KeyCode.E))
                smoothRotate.NewRotation = Quaternion.Euler(Vector3.up * smoothRotate.RotationSpeed + unityObjectComponent.UnitySceneObject.transform.rotation.eulerAngles);

            smoothTranslate.SetMovementDirection(input, unityObjectComponent.UnitySceneObject.transform);
        }
    }
}
