using System;
using JetBrains.Annotations;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components
{
    public class ClickableComponent : IComponent
    {
        public bool Clicked { get; private set; }
        [CanBeNull] public EventHandler OnClick;

        public void ClickOnMe()
        {
            Clicked = true;
            OnClick?.Invoke(this,EventArgs.Empty);
        }

        public void StateIsHandled()
        {
            Clicked = false;
        }
    }
}