using Core.Components.SmoothLookAtTargetComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities
{
    public class Bob : MonoEntity
    {
        [SerializeField] private SmoothLookAtTargetConfig _smoothLookAtTargetConfig;

        private void Start()
        {
            ContextAdd(new SmoothLookAtTarget(_smoothLookAtTargetConfig, this));
        }
    }
}