using System;
using Core.Components.RandomableComponent;
using UnityEngine;
using Wooff.ECS;

namespace Core.Components.WorldCellComponent.Cells
{
    [Serializable]
    public class WorldCellsConfig : IConfig
    {
        public Vector2Int GridSize;
        public float CellSize;
        public float CellHeightMaxOffset;
        public float CellHeightMinOffset;
        public RandomableConfig GenerateHeightOffset;
    }
}