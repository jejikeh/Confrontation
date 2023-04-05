using Core.Components;
using System;
using System.Collections.Generic;
using Core.Components.Tags;
using UnityEngine;
using Wooff.ECS.Entities;
using Random = UnityEngine.Random;

namespace Core
{
    [Serializable]
    public class CellGenerator
    {
        public Vector2Int GridSize;
        public float CellSize;
        public float CellHeightMaxOffset;
        public float CellHeightMinOffset;
        public RandomableComponent GenerateHeightOffset;

        private float _heightOffset;

        private Vector3 GetPositionForCellFromCoordinate(Vector2 coordinate)
        {
            var row = coordinate.y;
            var width = Mathf.Sqrt(3) * CellSize;
            var height = 2f * CellSize;
            var verticalDistance = height * (3f / 4f);
            var offset = row % 2 == 0 ? width / 2 : 0;
            return new Vector3(
                coordinate.x * width + offset,
                RandomHeightOffset(),
                -(row * verticalDistance));
        }

        private float RandomHeightOffset()
        {
            if (GenerateHeightOffset.GenerateMe())
            {
                var randomChoice = Random.Range(0, 2);
                switch (randomChoice)
                {
                    case 0:
                        _heightOffset -= Random.Range(CellHeightMinOffset, CellHeightMaxOffset);
                        break;
                    case 1:
                        _heightOffset += Random.Range(CellHeightMinOffset, CellHeightMaxOffset);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return _heightOffset;
        }

        public List<IEntity> InitWorld()
        {
            var entityContext = new List<IEntity>();
            for (var x = 0; x < GridSize.x; x++)
            {
                for (var y = 0; y < GridSize.y; y++)
                {
                    var position = GetPositionForCellFromCoordinate(new Vector2Int(x, y));
                    var randomCell = CellPrefabsHandler.RandomPlainCell();
                    var entity = new CellTagComponent(randomCell, position, Quaternion.Euler(new Vector3(0f,Random.Range(0,7) * 60,0f))).CreateCellEntityContainer();
                    entityContext.Add(entity); 
                }
            }

            return entityContext;
        }
    }
}
