using System;
using Core.Components.InformationComponent;
using Core.Components.UIComponents.ScreenComponent;
using Core.Entities.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows
{
    public class InformationWindow : Window
    {
        public override WindowType WindowType => WindowType.Information;

        private TMP_Text _heading;
        private TMP_Text _description;

        public InformationWindow(IConfig data, IMonoEntity handler) : base(data,
            handler)
        {
            _heading = Handler.MonoObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            _description = Handler.MonoObject.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        }

        protected override void OnClick(object sender, EventArgs e)
        {
            ScreenPlacer.CloseWindow(WindowType);
        }

        public void ShowInformation(Information information)
        {
            _heading.text = information.Config.Name;
            _description.text = information.Config.Description;
        }
    }
}