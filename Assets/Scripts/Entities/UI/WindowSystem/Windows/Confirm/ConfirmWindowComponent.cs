using System;
using System.Threading.Tasks;
using Entities.UI.ScreenSystem.Screens;
using Entities.UI.WindowSystem.WindowElements;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entities.UI.WindowSystem.Windows.Confirm
{
    public class ConfirmWindowComponent : WindowComponent<ConfirmWindowComponentConfig>
    {
        private WaitForSeconds _transitionPause;
        private readonly ButtonElement _confirmButton;
        private readonly ButtonElement _cancelButton;
        private readonly HeadingElement _windowTitle;
        
        public ConfirmWindowComponent(IScreenComponent screenComponent, UIDocument uiDocument) : base(screenComponent, uiDocument)
        {
            _confirmButton = new ButtonElement(ComponentConfig.ButtonAsset, "Confirm");
            _cancelButton = new ButtonElement(ComponentConfig.ButtonAsset, "Cancel");
            _windowTitle = new HeadingElement(ComponentConfig.HeadingAsset, "Are you sure?");
            _confirmButton.OnButtonClick += ConfirmButtonOnClick;
            _cancelButton.OnButtonClick += CancelButtonOnClick;
            
        }

        private async void CancelButtonOnClick(object sender, EventArgs e)
        {
            await ScreenManager.CloseWindowAsync<ConfirmWindowComponent>();
        }

        private void ConfirmButtonOnClick(object sender, EventArgs e)
        {
            Debug.Log("Hello World");
        }

        public override Task OnClose()
        {
            return Task.CompletedTask;
        }

        public override Task OnOpen()
        {
            Container("confirm-heading-container").Add(_windowTitle);
            Container("confirm-buttons-container").Add(_confirmButton);
            Container("confirm-buttons-container").Add(_cancelButton);
            return Task.CompletedTask;
        }
    }
}