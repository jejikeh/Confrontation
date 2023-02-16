using System;
using Core.Components.SmoothLookAtTargetComponent;
using Core.Components.SmoothRotateComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities
{
    [RequireComponent(typeof(Camera))]
    public class SmoothCamera : MonoEntity
    {
        [SerializeField] private SmoothLookAtTargetConfig _smoothLookAtTargetConfig;
        private SmoothLookAtTarget _smoothLookAtTarget;
        
        private void Start()
        {
            _smoothLookAtTarget = (SmoothLookAtTarget)ContextAdd(new SmoothLookAtTarget(_smoothLookAtTargetConfig, this));
        }

        private void Update()
        {
            _smoothLookAtTarget.UpdateOffset(Input.mouseScrollDelta.y * 10f);
        }
    }
}