using Core.Components.UIComponents.ScreenComponent;
using Core.Entities.UI;
using UnityEngine.UI;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows
{
    public class ToolBarWindow : Window
    {
        public override WindowType WindowType => WindowType.ToolBar;

        private readonly Button _noneToolButton;
        private readonly Button _infoToolButton;
        private readonly Button _buildToolButton;


        public ToolBarWindow(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            var monoTransform = Handler.MonoObject.transform;
            _noneToolButton = monoTransform.GetChild(0).GetComponent<Button>();
            _infoToolButton = monoTransform.GetChild(1).GetComponent<Button>();
            _buildToolButton = monoTransform.GetChild(2).GetComponent<Button>();
            
            _noneToolButton.onClick.AddListener(NoneToolButtonOnClicked);
            _infoToolButton.onClick.AddListener(InfoToolButtonOnClicked);
            _buildToolButton.onClick.AddListener(BuildToolButtonOnClicked);
        }

        private void NoneToolButtonOnClicked()
        {
            ScreenPlacer.SetScreenState(ScreenState.None);
            ScreenPlacer.CloseWindow(WindowType.BuildTool);
            ScreenPlacer.CloseWindow(WindowType.Information);
        }
        
        private void InfoToolButtonOnClicked()
        {
            ScreenPlacer.SetScreenState(ScreenState.Information);
            ScreenPlacer.CloseWindow(WindowType.BuildTool);
        }
        
        private void BuildToolButtonOnClicked()
        {
            ScreenPlacer.SetScreenState(ScreenState.Build);
            ScreenPlacer.CloseWindow(WindowType.Information);
            ScreenPlacer.GetWindow(WindowType.BuildTool);
        }

        public override void OnRemove()
        {
            _noneToolButton.onClick.RemoveAllListeners();
            _infoToolButton.onClick.RemoveAllListeners();
            _buildToolButton.onClick.RemoveAllListeners();
        }
    }
}