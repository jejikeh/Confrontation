using System;
using CustomComponents.SmoothLookAtTarget;
using CustomComponents.SmoothRotate;
using CustomComponents.SmoothTranslate;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Managers.PlayerInput;

namespace Entities.Camera
{
    public class SmoothCamera : Entity
    {
        [SerializeField] private SmoothTranslateComponentConfig _smoothTranslateComponentConfig;
        [SerializeField] private SmoothRotateComponentConfig _smoothRotateComponentConfig;
        [SerializeField] private SmoothLookAtTargetComponentConfig _smoothLookAtTargetComponentConfig;
        [SerializeField] private UnityEngine.Camera _camera;

        private SmoothTranslateComponent _smoothTranslateComponent;
        private SmoothRotateComponent _smoothRotateComponent;
        private SmoothLookAtTargetComponent _smoothLookAtTargetComponent;

        private Vector3 _startDrag;
        private bool _isFirstCallCalculateDragOffset;

        private void Start()
        {
            _smoothTranslateComponent = (SmoothTranslateComponent)AddCustomComponent(new SmoothTranslateComponent(_smoothTranslateComponentConfig));
            _smoothRotateComponent = (SmoothRotateComponent)AddCustomComponent(new SmoothRotateComponent(_smoothRotateComponentConfig));
            _smoothLookAtTargetComponent =
                (SmoothLookAtTargetComponent)AddCustomComponent(new SmoothLookAtTargetComponent(_smoothLookAtTargetComponentConfig));
            

            PlayerInput.Input.MouseDelta.performed += MouseDeltaPerform;
            PlayerInput.Input.ZoomCamera.performed += ZoomCameraPerform;
        }

        private void ZoomCameraPerform(InputAction.CallbackContext inputValue)
        {
            var value = -inputValue.ReadValue<Vector2>().y / 100f;
            _smoothLookAtTargetComponent.UpdateOffset(value);
        }

        private void MouseDeltaPerform(InputAction.CallbackContext inputValue)
        {
            // TODO: Separate work with mouse and transfer to it work with delta mouse
            // TODO: Added class to work with mouse and added modes 
            if (Mouse.current.middleButton.isPressed)
                RotateCameraAroundTarget(inputValue.ReadValue<Vector2>());

            if (Mouse.current.rightButton.isPressed && PlayerInput.Input.MouseDeltaStatement.phase == InputActionPhase.Performed)
                _smoothTranslateComponent.SetMovementDirection(
                    CalculateDragCameraVector(ref _isFirstCallCalculateDragOffset));
            else
                _isFirstCallCalculateDragOffset = true;
        }

        private void RotateCameraAroundTarget(Vector2 input)
        {
            input.y = input.x;
            input.x = 0;
            input = input.normalized;
            if (input.sqrMagnitude > 0.1f)
                _smoothRotateComponent.Rotate(input);
        }

        private void Update()
        {
            _smoothTranslateComponent.SetRelativeMovementDirection(PlayerInput.Input.Movement.ReadValue<Vector2>());
            UpdateCustomComponents();
        }

        private Vector3 CalculateDragCameraVector(ref bool isFirstCall)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!plane.Raycast(ray, out var distance)) 
                return Vector3.zero;

            if (isFirstCall)
            {
                _startDrag = ray.GetPoint(distance);
                // This need to prevent a bug when _startDrag is zero if using .wasPressedThisFrame 
                isFirstCall = false;
            }
            else
                return _startDrag - ray.GetPoint(distance);
            
            return Vector3.zero;
        }

        private void OnDisable()
        {
            PlayerInput.Input.MouseDelta.performed -= MouseDeltaPerform;
            PlayerInput.Input.ZoomCamera.performed -= ZoomCameraPerform;
        }
    }
}