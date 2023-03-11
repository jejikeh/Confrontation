using Core.Components.UiRelated.Windows.Information;
using Core.Components.UiRelated.Windows.ToolBox;
using Core.Components.UnityRelated;

namespace Core.Components.Tags.UiTags.Windows
{
    public class ToolBoxWindowTagComponent : WindowTagComponent<ToolBoxWindowComponent>, IWindowTagComponent
    {
        public ToolBoxWindowTagComponent(WindowTagComponentData windowTagComponentData)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(windowTagComponentData.UnityGameObjectComponent);
            WindowComponent = new ToolBoxWindowComponent();
        }
    }
}