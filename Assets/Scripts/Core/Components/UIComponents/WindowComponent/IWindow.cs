using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows
{
    public interface IWindow : IComponent<IConfig, IMonoEntity>
    {
        public WindowType WindowType { get; }
    }
}