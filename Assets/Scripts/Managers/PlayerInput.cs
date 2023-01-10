using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class PlayerInput : Singleton<PlayerInput>
    {
        public static CofrontationInput.PlayerInputActions Input => Instance._input.PlayerInput;
        private CofrontationInput _input;

        protected override void Awake()
        {
            base.Awake();
            _input ??= new CofrontationInput();
            _input.PlayerInput.Enable();
        }
    }
}