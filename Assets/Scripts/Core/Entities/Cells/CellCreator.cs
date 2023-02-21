using Core.Components.CellComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    public class CellCreator : MonoEntity
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _cellSize;
        [SerializeField] private float _cellHeight;
        private readonly float _heightOffset = 0;

        
        private void Start()
        {
            for (var x = 0; x < _gridSize.x; x++)
            {
                for (var y = 0; y < _gridSize.y; y++)
                {
                    var cellPresentation = MonoWorld.SpawnEntity<CellPresentation>();
                    cellPresentation.transform.position = GetPositionForCellFromCoordinate(new Vector2Int(x, y));
                    cellPresentation.transform.rotation = Quaternion.Euler(new Vector3(0f,Random.Range(0,7) * 60,0f));
                    cellPresentation.ContextAdd(Cell.RandomPlainCell(cellPresentation));
                    cellPresentation.transform.SetParent(transform, true);
                }
            }
        }

        private Vector3 GetPositionForCellFromCoordinate(Vector2Int coordinate)
        {
            var row = coordinate.y;
            var width = Mathf.Sqrt(3) * _cellSize;
            var height = 2f * _cellSize;
            var verticalDistance = height * (3f / 4f);
            var offset = (row % 2) == 0 ? width / 2 : 0;
            return new Vector3(
                coordinate.x * width + offset, 
                RandomHeightOffset(), 
                -(row * verticalDistance));
        }

        private float RandomHeightOffset()
        {
            var randomChoice = Random.Range(0, 3);
            return randomChoice switch
            {
                0 => _heightOffset,
                1 => _heightOffset - _cellHeight,
                2 => _heightOffset + _cellHeight,
                _ => _heightOffset
            };
        }
    }
}