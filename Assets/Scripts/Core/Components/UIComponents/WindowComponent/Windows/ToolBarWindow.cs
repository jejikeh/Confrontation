using Core.Components.UIComponents.ScreenComponent;
using Core.Entities.UI;
using UnityEngine.UI;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows
{
    public class ToolBarWindow : Window
    {
        public ToolBarWindow(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            var noneToolButton = Handler.MonoObject.transform.GetChild(0).GetComponent<Button>();
            var infoToolButton = Handler.MonoObject.transform.GetChild(1).GetComponent<Button>();
            var buildToolButton = Handler.MonoObject.transform.GetChild(2).GetComponent<Button>();
            
            noneToolButton.onClick.AddListener(NoneToolButtonOnClicked);
            infoToolButton.onClick.AddListener(InfoToolButtonOnClicked);
            buildToolButton.onClick.AddListener(BuildToolButtonOnClicked);
        }

        private void NoneToolButtonOnClicked()
        {
            ScreenPlacer.SetScreenState(ScreenState.None);
        }
        
        private void InfoToolButtonOnClicked()
        {
            ScreenPlacer.SetScreenState(ScreenState.Information);
        }
        
        private void BuildToolButtonOnClicked()
        {
            ScreenPlacer.SetScreenState(ScreenState.Build);
        }
        
        public override WindowType WindowType => WindowType.ToolBar;
    }
}