using Core.Components.UIComponents.ScreenComponent;
using Core.Components.UIComponents.WindowComponent;
using UnityEngine;
using Wooff.MonoIntegration;
using Screen = Core.Components.UIComponents.ScreenComponent.Screen;

namespace Core.Entities.UI
{
    public class WindowPlacer : StaticMonoEntity<WindowPlacer>
    {
        [SerializeField] private ScreenConfig _screenConfig;
        private void Start()
        {
            var screen = (Screen)ContextAdd(new Screen(_screenConfig, this));
            screen.OpenWindow(WindowType.Options);
        }
    }
}