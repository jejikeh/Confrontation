using System;
using UnityEngine;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.SmoothTranslateComponent
{
    [Serializable]
    public class SmoothTranslateConfig : IConfig
    {
        public float SmoothMovementTime;
        public float SpeedMovement;

        public Vector3 GetHandlerRight(IMonoEntity handler)
        {
            var right = handler.MonoObject.transform.right;
            right.y = 0;
            return right;
        }

        public Vector3 GetHandlerForward(IMonoEntity handler)
        {
            var forward = handler.MonoObject.transform.forward;
            forward.y = 0;
            return forward;
        }
        
        public Vector3 GetHandlerUp(IMonoEntity handler)
        {
            return handler.MonoObject.transform.up;
        }
    }
}