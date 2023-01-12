using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.WindowElements
{
    public abstract class WindowElement
    {
        public TemplateContainer ElementContainer;

        public WindowElement(VisualTreeAsset asset)
        {
            ElementContainer = asset.CloneTree();
        }
    }
}