using System;
using Core.Components.CellComponent;
using JetBrains.Annotations;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.ClickableComponent
{
    public class Clickable : Component<ClickableConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        public Clickable(ClickableConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        [CanBeNull] public EventHandler OnClick;

        // TODO: decide to keep this method or just invoke the event
        public void ClickOnMe()
        {
            OnClick?.Invoke(this, EventArgs.Empty);
        }
        
        IConfig IConfigurable<IConfig>.Config => Config;
    }
}