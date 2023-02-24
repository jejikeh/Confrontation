using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Core.Entities;
using Core.Entities.UI;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.ScreenComponent
{
    public class Screen : Component<ScreenConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>, IHashContext<IWindow>
    {
        IConfig IConfigurable<IConfig>.Config => Config; 
        private readonly WindowContext _windowContext;
        public HashSet<IWindow> Items => _windowContext.Items;

        public Screen(ScreenConfig data, IMonoEntity handler) : base(data, handler)
        {
            _windowContext = new WindowContext();
        }
        
        public IWindow ContextAdd(IWindow item)
        {
            return _windowContext.ContextAdd(item);
        }

        public IWindow OpenWindow(WindowType windowType)
        {
            if (ContextContains(windowType))
                return default;
            var window = Config.GetWindow(windowType);
            var monoItem = StaticMonoWorldFinder.SpawnEntity<MonoWindow>(Handler, window.gameObject);
            return windowType switch
            {
                WindowType.Information => ContextAdd((InformationWindow)monoItem.ContextAdd(new InformationWindow(null, monoItem))),
                WindowType.Metrics => ContextAdd((MetricsWindow)monoItem.ContextAdd(new MetricsWindow(null,monoItem))),
                WindowType.ToolBar => ContextAdd((ToolBarWindow)monoItem.ContextAdd(new ToolBarWindow(null,monoItem))),
                _ => throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null)
            };
        }
        
        public void CloseWindow(WindowType windowType)
        {   
            var windowComponent =
                _windowContext.Items.FirstOrDefault(windowContextItem => windowContextItem.WindowType == windowType);
            ContextRemove(windowComponent);
            StaticMonoWorldFinder.DestroyEntity(windowComponent?.Handler);
        }

        public T2 ContextGet<T2>() where T2 : class, IWindow
        {
            return _windowContext.ContextGet<T2>();
        }

        public IWindow ContextGet(WindowType windowType)
        {
            return _windowContext.Items.FirstOrDefault(windowContextItem => windowContextItem.WindowType == windowType);
        }

        public bool ContextRemove(IWindow item)
        {
            return _windowContext.ContextRemove(item);
        }

        public bool ContextContains<T2>() where T2 : class, IWindow
        {
            return _windowContext.ContextContains<T2>();
        }

        public bool ContextContains(WindowType windowType)
        {
            return _windowContext.Items.Any(windowContextItem => windowContextItem.WindowType == windowType);
        }

        public void SetScreenState(ScreenState screenState)
        {
            Config.ScreenState = screenState;
        }
    }
}