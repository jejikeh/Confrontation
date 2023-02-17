using System.Collections.Generic;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.SmoothTranslateComponent;
using Core.Entities.Camera;
using JetBrains.Annotations;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class CameraTranslateToLastClickedGameItem : IMonoSystem
    {
        [CanBeNull] private Clickable _lastClickableItemTarget;
        private SmoothTranslate _smoothTranslate;
        private Click _click;

        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            _smoothTranslate ??= data.ContextGet<CameraTarget>().ContextGet<SmoothTranslate>();
            _click ??= data.ContextGet<SmoothCamera>().ContextGet<Click>();

            if (_click.Config.LastClickable == _lastClickableItemTarget) 
                return;
            
            _lastClickableItemTarget = _click.Config.LastClickable;
            _smoothTranslate.SetPosition(_lastClickableItemTarget!.Handler.MonoObject.transform.position);
        }
    }
}