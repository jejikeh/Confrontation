using Entities.UI.WindowSystem.WindowElements;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem
{
    public static class WindowExtensions
    {
        public static void Add(this VisualElement visualElement, WindowElement windowElement)
        {
            visualElement.Add(windowElement.ElementContainer);
        }
    }
}