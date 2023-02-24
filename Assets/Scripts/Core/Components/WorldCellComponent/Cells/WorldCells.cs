using System;
using System.Collections.Generic;
using Core.Components.CellComponent;
using Core.Components.RandomableComponent;
using Core.Entities;
using Core.Entities.Cells;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;
using Random = UnityEngine.Random;

namespace Core.Components.WorldCellComponent.Cells
{
    public class WorldCells : 
        Component<WorldCellsConfig, IMonoEntity>, 
        IComponent<IConfig, IMonoEntity>, IListContext<ICell>
    {
        IConfig IConfigurable<IConfig>.Config => Config; 
        private float _heightOffset;
        private readonly CellContext _cellContext;

        public WorldCells(WorldCellsConfig data, IMonoEntity handler) : base(data, handler)
        {
            _cellContext = new CellContext();
        }

        public void InitWorld()
        {
            for (var x = 0; x < Config.GridSize.x; x++)
            {
                for (var y = 0; y < Config.GridSize.y; y++)
                {
                    var cellPresentation = StaticMonoWorldFinder.SpawnEntity<CellPresentation>(typeof(CellPresentation).FullName);
                    cellPresentation.transform.position = GetPositionForCellFromCoordinate(new Vector2Int(x, y));
                    cellPresentation.transform.rotation = Quaternion.Euler(new Vector3(0f,Random.Range(0,7) * 60,0f));
                    cellPresentation.transform.SetParent(Handler.MonoObject.transform, true);
                    
                    ContextAdd((ICell)cellPresentation.ContextAdd(Cell.RandomPlainCell(cellPresentation)));
                }
            }
        }
        
        public ICell ChangeRandomCellTo(CellType cellType)
        {
            while (true)
            {
                var cell = GetRandomCellPlainCell();
                if (cell?.Config.CellType == cellType) 
                    continue;
                
                return cell?.ChangeToCellType(cellType);
            }
        }

        public Cell GetRandomCellPlainCell()
        {
            while (true)
            {
                var cell = Items[Random.Range(0, Items.Count - 1)] as Cell;
                if (!cell!.Config.PlainCell || cell.Config.CellType is CellType.City or CellType.Village )
                    continue;
                
                return cell;
            }
        }

        private Vector3 GetPositionForCellFromCoordinate(Vector2 coordinate)
        {
            var row = coordinate.y;
            var width = Mathf.Sqrt(3) * Config.CellSize;
            var height = 2f * Config.CellSize;
            var verticalDistance = height * (3f / 4f);
            var offset = (row % 2) == 0 ? width / 2 : 0;
            return new Vector3(
                coordinate.x * width + offset, 
                RandomHeightOffset(), 
                -(row * verticalDistance));
        }

        private float RandomHeightOffset()
        {
            if (Randomable.GenerateThis(Config.GenerateHeightOffset))
            {
                var randomChoice = Random.Range(0, 2);
                switch (randomChoice)
                {
                    case 0:
                        _heightOffset -= Random.Range(Config.CellHeightMinOffset, Config.CellHeightMaxOffset);
                        break;
                    case 1:
                        _heightOffset += Random.Range(Config.CellHeightMinOffset, Config.CellHeightMaxOffset);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return _heightOffset;
        }

        public List<ICell> Items => _cellContext.Items;

        public ICell ContextAdd(ICell item)
        {
            return _cellContext.ContextAdd(item);
        }

        public T2 ContextGet<T2>() where T2 : class, ICell
        {
            return _cellContext.ContextGet<T2>();
        }

        public bool ContextRemove(ICell item)
        {
            return _cellContext.ContextRemove(item);
        }

        public bool ContextContains<T2>() where T2 : class, ICell
        {
            return _cellContext.ContextContains<T2>();
        }
    }
}