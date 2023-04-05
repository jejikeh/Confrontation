using System.Linq;
using Core.Components.CellRelated;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.Tags;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Components.UiRelated.Windows.ChooseCell
{
    public class ChooseCellWindowComponent : WindowComponent
    {
        public IEntity ClickedEntity { get; private set; }
        public EntityContext EntityContext { get; private set; }

        public ChooseCellWindowComponent(IEntity clickedCell, EntityContext entityContext)
        {
            ClickedEntity = clickedCell;
            EntityContext = entityContext;
        }

        public void UpdateClickedCell(IEntity clickedCell)
        {
            ClickedEntity = clickedCell;
        }
        
        
    }
}