using Core.Components.UiRelated;
using Core.Components.UnityRelated;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags
{
    public class WindowContextTagComponent : IComponent
    {
        public UnityGameObjectComponent UnityGameObjectComponent;
        public UnityCanvasComponent UnityCanvasComponent;
        public WindowContextComponent WindowContextComponent;

        public WindowContextTagComponent(UnityGameObjectComponent unityGameObjectComponent)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(unityGameObjectComponent);
            UnityCanvasComponent = new UnityCanvasComponent();
            WindowContextComponent = new WindowContextComponent();
        }

        public IEntity CreateWindowContextEntityContainer()
        {
            return new Entity(
                UnityGameObjectComponent,
                UnityCanvasComponent,
                WindowContextComponent,
                this);
        }
    }
}