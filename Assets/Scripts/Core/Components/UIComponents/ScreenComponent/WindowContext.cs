using System.Collections.Generic;
using Core.Components.UIComponents.WindowComponent;
using Wooff.ECS.Contexts;

namespace Core.Components.UIComponents.ScreenComponent
{
    public class WindowsContext : Context<IWindow, List<IWindow>>, IListContext<IWindow> 
    {
    }
}