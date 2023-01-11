using System;
using Core.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CustomComponents.SmoothRotate
{
    [Serializable]
    public class SmoothRotateComponentConfig : ICustomComponentConfig
    {
        public Transform Handler;
        public float RotationSpeed;
        public float RotationTime;
    }
}