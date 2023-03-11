using Core.Components.UiRelated.Windows.ChooseCell;
using Core.Components.UnityRelated;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Components.Tags.UiTags.Windows
{
    public class ChooseCellWindowTagComponent : WindowTagComponent<ChooseCellWindowComponent>, IWindowTagComponent
    {
        public ChooseCellWindowTagComponent(IEntity clickedCell, EntityContext entityContext, WindowTagComponentData windowTagComponentData)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(windowTagComponentData.UnityGameObjectComponent);
            WindowComponent = new ChooseCellWindowComponent(clickedCell, entityContext);
        }
    }
}