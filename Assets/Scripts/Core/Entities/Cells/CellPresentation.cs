using Core.Components.CellComponent;
using Wooff.MonoIntegration;

namespace Core.Entities.Cell
{
    public class CellPresentation : MonoEntity
    {
        [SerializeField] private CellConfig _cellConfig;
        private Cell _smoothLookAtTarget;
        
        private void Start()
        {
            _smoothLookAtTarget = (SmoothLookAtTarget)ContextAdd(new SmoothLookAtTarget(_smoothLookAtTargetConfig, this));
        }
    }
}