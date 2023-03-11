using System;
using Wooff.ECS.Components;

namespace Core.Components.CellRelated
{
    [Serializable]
    public class CellComponent : IComponent
    {
        public bool Plain;
        public CellType CellType;
    }
}