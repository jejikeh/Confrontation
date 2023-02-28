using Core.Components.ClickableComponent;
using JetBrains.Annotations;
using Wooff.ECS;

namespace Core.Components.ClickComponent
{
    public class ClickConfig : IConfig
    {
        [CanBeNull] public Clickable LastClickable;
        // TODO: Create way to change CurrentActiveLayer base on mouse position
        public ClickLayer CurrentActiveLayer;
    }
}