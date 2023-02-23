using Core.Components.CellComponent;
using Core.Components.PlayerComponent;
using Core.Components.WorldCellComponent.Cells;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    public class CellWorldCreator : StaticMonoEntity<CellWorldCreator>
    {
        [SerializeField] private WorldCellsConfig _worldCellsConfig;
        private WorldCells _worldCells;
        
        private void Start()
        {
            _worldCells = (WorldCells)ContextAdd(new WorldCells(_worldCellsConfig, this));
            _worldCells.InitWorld();
        }

        public static Cell GetRandomCell()
        {
            return Instance._worldCells.GetRandomCellPlainCell();
        }
    }
}