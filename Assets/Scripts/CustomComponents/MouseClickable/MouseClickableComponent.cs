using System;
using Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomComponents.MouseClickable
{
    public class MouseClickableComponent : CustomComponent
    {
        // TODO: Chech naming convenrtions
        [CanBeNull] public event EventHandler OnMouseClicked;
        private readonly Camera _camera;
        private readonly Transform _transform;
        
        public MouseClickableComponent(Camera camera, Transform handler)
        {
            _camera = camera;
            _transform = handler;
        }

        protected override void OnUpdate(float timeScale)
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame)
                return;

            var ray = 
                _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit, 1000f))
                if(hit.transform == _transform)
                    OnMouseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}