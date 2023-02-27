using System.Collections.Generic;
using System.Linq;
using Core.Components.CellComponent;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.PlayerComponent;
using Core.Components.SmoothTranslateComponent;
using Core.Components.UIComponents.ScreenComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows.Tools;
using Core.Entities.Camera;
using Core.Entities.UI;
using JetBrains.Annotations;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class PlayerBuyCell : IMonoSystem
    {
        [CanBeNull] private Clickable _lastClickableItemTarget;
        private Click _click;

        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            
            if (ScreenPlacer.GetScreenState() == ScreenState.Build)
            {
                _click ??= data.ContextGet<SmoothCamera>().ContextGet<Click>();

                if (_click.Config.LastClickable == _lastClickableItemTarget)
                    return;

                if (_click.Config.LastClickable == null)
                    return;

                _lastClickableItemTarget = _click.Config.LastClickable;
                if (_lastClickableItemTarget.Handler.MonoObject == null)
                    return;

                if (!_lastClickableItemTarget.Handler.ContextContains<Cell>()) 
                    return;
                
                var playerEntity = data.Items.FirstOrDefault(x => x.ContextGetAs<Player>().Config.PlayerType == PlayerType.User);
                playerEntity?.ContextGetAs<Player>().BuyCell(
                    _lastClickableItemTarget.Handler.ContextGet<Cell>(), 
                    ((BuildTool)ScreenPlacer.GetWindow(WindowType.BuildTool)).SelectedCellType);
            }
        }
    }
}