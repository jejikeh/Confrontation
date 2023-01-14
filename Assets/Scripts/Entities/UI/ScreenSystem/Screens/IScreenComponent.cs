using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Entities.UI.WindowSystem.Windows;

namespace Entities.UI.ScreenSystem.Screens
{
    public interface IScreenComponent : IConstantStateComponent
    {
        public ScreenComponentConfig ScreenComponentConfig { get; }
        // TODO: WIP, maybe remove GameObject dependency and think about it
        public Task OnOpen();
        public Task OnClose();
        public Task OpenWindowAsync(IWindowComponent windowComponent, string containerName);
        public Task CloseWindow<T>() where T : class, IWindowComponent;
        //public void ShowWindow(IWindowComponent windowComponent);
        //public List<IWindowComponent> OpenedWindows();
    }
}