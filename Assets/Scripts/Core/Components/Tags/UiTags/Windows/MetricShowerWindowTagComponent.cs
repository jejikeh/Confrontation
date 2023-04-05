using Core.Components.UiRelated.Windows.Information;
using Core.Components.UiRelated.Windows.MetricShower;
using Core.Components.UiRelated.Windows.ToolBox;
using Core.Components.UnityRelated;

namespace Core.Components.Tags.UiTags.Windows
{
    public class MetricShowerWindowTagComponent : WindowTagComponent<MetricShowerWindowComponent>, IWindowTagComponent
    {
        public MetricShowerWindowTagComponent(WindowTagComponentData windowTagComponentData)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(windowTagComponentData.UnityGameObjectComponent);
            WindowComponent = new MetricShowerWindowComponent();
        }
    }
}