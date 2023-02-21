using Core.Components.UIComponents.ScreenComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using UnityEngine;
using Wooff.MonoIntegration;
using Screen = Core.Components.UIComponents.ScreenComponent.Screen;

namespace Core.Entities.UI
{
    public class ScreenPlacer : StaticMonoEntity<ScreenPlacer>
    {
        [SerializeField] private ScreenConfig _screenConfig;
        private void Start()
        {
            ContextAdd(new Screen(_screenConfig, this));
        }

        public static IWindow GetWindow(WindowType windowType)
        {
            if (!Instance.ContextGet<Screen>().ContextContains(windowType))
                return OpenWindow(windowType);

            return Instance.ContextGet<Screen>().ContextGet(windowType);
        }
        
        public static IWindow OpenWindow(WindowType windowType)
        {
            return Instance.ContextGet<Screen>().OpenWindow(windowType);
        }
        
        public static void CloseWindow(WindowType windowType) 
        {
            Instance.ContextGet<Screen>().CloseWindow(windowType);
        }
    }
}