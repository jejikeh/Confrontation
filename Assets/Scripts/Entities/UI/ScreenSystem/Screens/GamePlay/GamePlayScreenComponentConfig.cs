using TMPro;
using UnityEngine;

namespace Entities.UI.ScreenSystem.Screens.GamePlay
{
    [CreateAssetMenu(fileName = "GamePlayScreenConfig", menuName = "UISystem/Screens/GamePlay", order = 0)]
    public class GamePlayScreenComponentConfig : ScreenComponentConfig
    {
        public TMP_Text TestText;
    }
}