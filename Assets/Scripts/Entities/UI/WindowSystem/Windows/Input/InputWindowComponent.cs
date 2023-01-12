using System.Threading.Tasks;
using Entities.UI.ScreenSystem.Screens;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows.Input
{
    public class InputWindowComponent : WindowComponent<InputWindowComponentConfig>
    {
        public InputWindowComponent(IScreenComponent screenComponent, UIDocument uiDocument) : base(screenComponent, uiDocument)
        {
        }
        
        public override Task OnClose()
        {
            return Task.CompletedTask;
        }

        public override Task OnOpen()
        {
            return Task.CompletedTask;
        }
    }
}