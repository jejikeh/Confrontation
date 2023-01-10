using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Interfaces;
using Entities.UI.WindowSystem.Windows;
using UnityEngine.UIElements;

namespace Entities.UI.ScreenSystem.Screens
{
    public abstract class ScreenComponent<T> : CustomComponent<T>, ICustomComponentHandler<IWindowComponent>, IScreenComponent where T : ScreenComponentConfig
    {
        // TODO: Check for what that need
        public ScreenComponentConfig ScreenComponentConfig => ComponentConfig;
        public List<IWindowComponent> CustomComponents { get; set; } = new List<IWindowComponent>();
        protected readonly UIDocument UIDocument;
        
        protected ScreenComponent(T customComponentConfig, UIDocument uiDocument) : base(customComponentConfig)
        {
            UIDocument = uiDocument;
            uiDocument.visualTreeAsset = ComponentConfig.VisualTreeAsset;
            uiDocument.panelSettings = ComponentConfig.PanelSettings;
        }

        // TODO: Create custom exceptions
        public async Task OpenWindowAsync(IWindowComponent windowComponent)
        {
            var window = AddCustomComponent(windowComponent);
            await window.OnOpen();
        }

        public async Task CloseWindow<T1>() where T1 : class, IWindowComponent
        {
            var window = GetCustomComponent<T1>();
            await window?.OnClose()!;
            RemoveCustomComponent(window);
        }
        
        public IWindowComponent AddCustomComponent(IWindowComponent component)
        {
            var requiredComponent = CustomComponents.FirstOrDefault(x => component.GetType() == x.GetType());
            // TODO: not throw exception like that
            if (requiredComponent is not null)
                throw new Exception($"The {component.GetType().FullName} already attached to this component");

            component.InitWindow(UIDocument.rootVisualElement.Q<VisualElement>(component.WindowComponentConfig.ScreenContainerName));
            CustomComponents.Add(component);
            component.Enable();
            return component;
        }

        public void RemoveCustomComponent(IWindowComponent component)
        {
            var customComponent = CustomComponents.FirstOrDefault(x => x == component);
            if (customComponent is null)
                throw new Exception($"{component.GetType().FullName} is not attached to this entity");
            
            customComponent.Disable();
            customComponent.Destroy();
            component.HolderContainer.Remove(component.RootContainer);
            CustomComponents.Remove(customComponent);
        }

        public void RemoveCustomComponent<T1>() where T1 : class, IWindowComponent
        {
            var tempComponent = GetCustomComponent<T1>();
            tempComponent?.HolderContainer.Remove(tempComponent.RootContainer);
            tempComponent?.Disable();
            tempComponent?.Destroy();
            CustomComponents.Remove(tempComponent);
        }

        public T1 GetCustomComponent<T1>() where T1 : class, IWindowComponent
        {
            var requiredComponent = CustomComponents.FirstOrDefault(x => typeof(T1) == x.GetType());
            if (requiredComponent is null)
                throw new Exception($"The window {typeof(T1)} of this class doesnt attached to this component");

            return requiredComponent as T1;
        }

        public void UpdateCustomComponents(float timeScale)
        {
            foreach (var component in CustomComponents)
                component.Update(timeScale);
        }
        
        public abstract Task OnOpen();
        public abstract Task OnClose();
    }
}