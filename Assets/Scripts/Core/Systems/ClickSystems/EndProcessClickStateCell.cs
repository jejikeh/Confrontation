using Core.Components;
using Core.Components.Tags;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems.ClickSystems
{
    public class EndProcessClickStateCell : HandleClickedState<CellTagComponent>
    {
        protected override void ProcessClickedEntity(EntityContext context, IEntity clickedEntity)
        {
            clickedEntity.ContextGet<ClickableComponent>().StateIsHandled();
        }
    }
}