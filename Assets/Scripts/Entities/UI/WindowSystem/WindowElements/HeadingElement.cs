using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.WindowElements
{
    public class HeadingElement : WindowElement
    {
        public HeadingElement(VisualTreeAsset asset, string text) : base(asset)
        {
            var heading = ElementContainer.Q<Label>(asset.name);
            heading.text = text;
        }
    }
}