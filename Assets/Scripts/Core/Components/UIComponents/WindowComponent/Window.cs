using System;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent
{
    public abstract class Window : Component<IConfig, IMonoEntity>, IWindow
    {
        protected Window(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            var clickable = (Clickable)Handler.ContextAdd(new Clickable(
                new ClickableConfig()
                {
                    ClickLayer = ClickLayer.UI
                },
                Handler));
            
            clickable.OnClick += OnClick;
        }

        protected virtual void OnClick(object sender, EventArgs e)
        {
        }

        public abstract WindowType WindowType { get; }
    }
}