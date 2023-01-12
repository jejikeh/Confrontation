using UnityEngine;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows.Confirm
{
    [CreateAssetMenu(fileName = "ConfirmWindowConfig", menuName = "UISystem/Windows/Confirm", order = 0)]

    public class ConfirmWindowComponentConfig : WindowComponentConfig
    {
        [SerializeField] public VisualTreeAsset ButtonAsset;
        [SerializeField] public VisualTreeAsset HeadingAsset;
    }
}