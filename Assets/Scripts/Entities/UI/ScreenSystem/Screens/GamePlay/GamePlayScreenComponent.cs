using System.Threading.Tasks;
using Entities.UI.WindowSystem.Windows.Confirm;
using UnityEngine.UIElements;

namespace Entities.UI.ScreenSystem.Screens.GamePlay
{
    public class GamePlayScreenComponent : ScreenComponent<GamePlayScreenComponentConfig>
    {
        public GamePlayScreenComponent(GamePlayScreenComponentConfig customComponentConfig, UIDocument uiDocument) 
            : base(customComponentConfig, uiDocument)
        {
        }
        
        public override async Task OnOpen()
        {
            await OpenWindowAsync(
                new ConfirmWindowComponent(this, UIDocument),"global-container");
        }
        
        public override Task OnClose()
        {
            return Task.CompletedTask;
        }
    }
}