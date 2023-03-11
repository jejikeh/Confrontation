using Core.Components.UiRelated.Windows.Information;
using Core.Components.UnityRelated;

namespace Core.Components.Tags.UiTags.Windows
{
    public class InformationWindowTagComponent : WindowTagComponent<InformationWindowComponent>, IWindowTagComponent
    {
        public InformationWindowTagComponent(InformationComponent informationComponent, WindowTagComponentData windowTagComponentData)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(windowTagComponentData.UnityGameObjectComponent);
            WindowComponent = new InformationWindowComponent(informationComponent);
        }
    }
}