using Core.Components.UiRelated.Windows.Information;
using Core.Components.UnityRelated;

namespace Core.Components.Tags.UiTags.Windows
{
    public class InformationTagComponent : WindowTagComponent<InformationWindowComponent>, IWindowTagComponent
    {
        public InformationTagComponent(InformationComponent informationComponent, WindowTagComponentData windowTagComponentData)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(windowTagComponentData.UnityGameObjectComponent);
            WindowComponent = new InformationWindowComponent(informationComponent);
        }
    }
}