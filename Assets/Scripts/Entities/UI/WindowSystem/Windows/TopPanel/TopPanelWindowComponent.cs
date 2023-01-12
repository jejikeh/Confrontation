using System.Threading.Tasks;
using Entities.UI.ScreenSystem.Screens;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows.TopPanel
{
    public class TopPanelWindowComponent : WindowComponent<TopPanelWindowComponentConfig>
    {
        public TopPanelWindowComponent(IScreenComponent screenComponent, UIDocument uiDocument) : base(screenComponent, uiDocument)
        {
        }
        
        public override Task OnClose()
        {
            throw new System.NotImplementedException();
        }

        public override Task OnOpen()
        {
            throw new System.NotImplementedException();
        }
    }
}