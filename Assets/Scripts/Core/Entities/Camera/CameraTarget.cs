using Core.Components.SmoothLookAtTargetComponent;
using Core.Components.SmoothRotateComponent;
using Core.Components.SmoothTranslateComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Camera
{
    public class CameraTarget : MonoEntity
    {
        [SerializeField] private SmoothRotateConfig _smoothRotateConfig;
        private SmoothRotate _smoothRotate;
        
        [SerializeField] private SmoothTranslateConfig _smoothTranslateConfig;
        private SmoothTranslate _smoothTranslate;
        
        private void Start()
        {
            _smoothTranslate = (SmoothTranslate)ContextAdd(new SmoothTranslate(_smoothTranslateConfig, this));
            _smoothRotate = (SmoothRotate)ContextAdd(new SmoothRotate(_smoothRotateConfig, this));
            ContextAdd(new TargetLootAt(null, this));
        }

        private void Update()
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _smoothTranslate.SetMovementDirection(input);
            
            if (Input.GetKey(KeyCode.Q))
                _smoothRotate.Rotate(new Vector3(0, -1, 0));
            else if(Input.GetKey(KeyCode.E))
                _smoothRotate.Rotate(new Vector3(0,1,0));
        }
    }
}