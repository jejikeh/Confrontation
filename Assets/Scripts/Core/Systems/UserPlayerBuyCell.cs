using System.Collections.Generic;
using System.Linq;
using Core.Components.CellComponent;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.PlayerComponent;
using Core.Components.PlayerComponent.Players;
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
    public class UserPlayerBuyCell : IMonoSystem
    {
        [CanBeNull] private Clickable _lastClickableItemTarget;
        private Click _click;

        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            
            if (ScreenPlacer.GetScreenState() == ScreenState.Build)
            {
                var userEntity = data.Items.FirstOrDefault(x => x.ContextContains<User>());

                if (userEntity is null || TurnToMove.HisMove != userEntity.ContextGetAs<Player>())
                    return;
                
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
                
                userEntity.ContextGet<User>().BuyCell(
                    _lastClickableItemTarget.Handler.ContextGet<Cell>(), 
                    ((BuildTool)ScreenPlacer.GetWindow(WindowType.BuildTool)).SelectedCellType);
            }
        }
    }
}