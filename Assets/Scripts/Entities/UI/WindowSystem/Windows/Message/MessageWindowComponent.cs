using System.Threading.Tasks;
using Entities.UI.ScreenSystem.Screens;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows.Message
{
    public class MessageWindowComponent : WindowComponent<MessageWindowComponentConfig>
    {

        public MessageWindowComponent(IScreenComponent screenComponent, UIDocument uiDocument) : base(screenComponent, uiDocument)
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