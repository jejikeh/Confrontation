using Core.Components.CellComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Core.Components.WorldCellComponent.Cells;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    public class CellWorldCreator : MonoEntity
    {
        private WorldCells _worldCells;
        
        private void Start()
        {
            _worldCells = (WorldCells)ContextAdd(new WorldCells(CellManager.WorldCellsConfig, this));
            _worldCells.InitWorld();
        }

        public Cell GetRandomCell()
        {
            return _worldCells.GetRandomCellPlainCell();
        }

        public Cell GetFreeCellForBuy(PropertyHandler propertyHandler)
        {
            return _worldCells.GetFreeCellForBuy(propertyHandler);
        }
    }
}