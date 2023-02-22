using Core.Components.CellComponent;
using Core.Components.WorldCellComponent.Cells;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    public class CellWorldCreator : MonoEntity
    {
        [SerializeField] private int _players;
        [SerializeField] private WorldCellsConfig _worldCellsConfig;

        private void Start()
        {
            var worldCells = (WorldCells)ContextAdd(new WorldCells(_worldCellsConfig, this));
            worldCells.InitWorld();
            for (int i = 0; i < _players; i++)
                worldCells.ChangeRandomCellTo(CellType.City);
        }
    }
}