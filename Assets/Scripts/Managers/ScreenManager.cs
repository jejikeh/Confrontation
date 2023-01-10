using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.UI.ScreenSystem;
using Entities.UI.ScreenSystem.Screens;
using Entities.UI.ScreenSystem.Screens.GamePlay;
using Entities.UI.WindowSystem;
using Entities.UI.WindowSystem.Windows;
using Entities.UI.WindowSystem.Windows.Confirm;
using Entities.UI.WindowSystem.Windows.Input;
using Entities.UI.WindowSystem.Windows.Message;
using Entities.UI.WindowSystem.Windows.TopPanel;
using JetBrains.Annotations;
using UnityEngine;
using Screen = Entities.UI.ScreenSystem.Screen;

namespace Managers
{
    // TODO: Combine old WindowManager with new system like in BuildingSystem and use UIUserComponent
    public class ScreenManager : Core.Singleton<ScreenManager>
    {
        [SerializeField] private List<ScreenComponentConfig> _screenComponentConfigs = new List<ScreenComponentConfig>();
        [SerializeField] public Screen CurrentScreen;

        [CanBeNull] public static IScreenComponent GetCurrentScreenComponent() => Instance.CurrentScreen.GetConstantState();

        public static async Task OpenWindowAsync(WindowType type)
        {
            var currentScreenComponent = GetCurrentScreenComponent();

            if (currentScreenComponent is null)
                return;
            
            switch (type)
            {
                case WindowType.Confirm:
                    await currentScreenComponent.OpenWindowAsync(
                        new ConfirmWindowComponent(
                            currentScreenComponent,
                            Instance.CurrentScreen.UIDocument));
                    break;
                case WindowType.Message:
                    await currentScreenComponent.OpenWindowAsync(
                        new MessageWindowComponent(
                            currentScreenComponent,
                            Instance.CurrentScreen.UIDocument));
                    break;
                case WindowType.Input:
                    await currentScreenComponent.OpenWindowAsync(
                        new InputWindowComponent(
                            currentScreenComponent,
                            Instance.CurrentScreen.UIDocument));
                    break;
                case WindowType.TopPanel:
                    await currentScreenComponent.OpenWindowAsync(
                        new TopPanelWindowComponent(
                            currentScreenComponent,
                            Instance.CurrentScreen.UIDocument));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static async Task CloseWindowAsync<T>() where T : class, IWindowComponent
        {
            await GetCurrentScreenComponent()?.CloseWindow<T>()!;
        }
        
        // TODO: Combine 2 methods or cache result in CreateScreenComponent
        public static async Task OpenScreen(ScreenType screenType)
        {
            await Instance.CurrentScreen.SetConstantState(CreateScreenComponent(screenType));
        }
        
        private async void Start()
        {
            await OpenScreen(ScreenType.GamePlay);
        }

        private T FindComponentConfig<T>() where T : ScreenComponentConfig
        {
            var requiredConfig = _screenComponentConfigs.FirstOrDefault(x => typeof(T) == x.GetType());
            if (requiredConfig is null)
                throw new Exception("The config was not find in BuildSystemConfig");

            return requiredConfig as T;
        }
        
        private static IScreenComponent CreateScreenComponent(ScreenType screenType)
        {
             var screenComponentConfig = Instance._screenComponentConfigs.FirstOrDefault(x => x.Type == screenType);
            if (screenComponentConfig == null)
                throw new Exception("The config isnt apear in ScreenManager");
            return screenType switch
            {
                ScreenType.GamePlay => new GamePlayScreenComponent(
                    Instance.FindComponentConfig<GamePlayScreenComponentConfig>(), 
                    Instance.CurrentScreen.UIDocument),
                _ => throw new ArgumentException($"{screenType} is wrong")
            };
        }
    }
}