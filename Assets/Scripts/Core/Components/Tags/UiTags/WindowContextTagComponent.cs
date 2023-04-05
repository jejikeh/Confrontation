using Core.Components.UiRelated;
using Core.Components.UnityRelated;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags.UiTags
{
    public class WindowContextTagComponent : IComponent
    {
        public UnityGameObjectComponent UnityGameObjectComponent;
        public UnityCanvasComponent UnityCanvasComponent;

        public WindowContextTagComponent(UnityGameObjectComponent unityGameObjectComponent)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(unityGameObjectComponent);
            UnityCanvasComponent = new UnityCanvasComponent();
        }

        public IEntity CreateWindowContextEntityContainer()
        {
            return new Entity(
                UnityGameObjectComponent,
                UnityCanvasComponent,
                this);
        }
    }
}