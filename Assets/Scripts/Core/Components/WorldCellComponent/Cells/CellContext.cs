using System.Collections.Generic;
using Core.Components.CellComponent;
using Wooff.ECS.Contexts;

namespace Core.Components.WorldCellComponent.Cells
{
    public class CellContext : Context<ICell, List<ICell>>, IListContext<ICell>
    {
        
    }
}