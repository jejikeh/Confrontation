using System.Collections.Generic;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Wooff.ECS.Contexts;

namespace Core.Components.UIComponents.ScreenComponent
{
    public class WindowContext : Context<IWindow, HashSet<IWindow>>, IHashContext<IWindow>
    {
    }
}