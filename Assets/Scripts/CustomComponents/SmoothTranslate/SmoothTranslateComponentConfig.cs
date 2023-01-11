using System;
using Core.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomComponents.SmoothTranslate
{
    [Serializable]
    public class SmoothTranslateComponentConfig : ICustomComponentConfig
    {
        public float SmoothMovementTime;
        public Transform Handler;
        public float SpeedMovement;

        public Vector3 GetHandlerRight()
        {
            var right = Handler.right;
            right.y = 0;
            return right;
        }

        public Vector3 GetHandlerForward()
        {
            var forward = Handler.forward;
            forward.y = 0;
            return forward;
        }
        
        public Vector3 GetHandlerUp()
        {
            return Handler.up;
        }
    }
}