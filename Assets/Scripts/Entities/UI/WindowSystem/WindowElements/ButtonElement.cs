using System;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.WindowElements
{
    public class ButtonElement : WindowElement
    {
        // TODO: cleate class Element and add to it methods like on destroy and etc.
        public event EventHandler OnButtonClick;
        
        public ButtonElement(VisualTreeAsset asset, string text) : base(asset)
        {
            var button = ElementContainer.Q<Button>(asset.name);
            button.text = text;
            button.clicked += ButtonOnClick;
        }

        private void ButtonOnClick()
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}