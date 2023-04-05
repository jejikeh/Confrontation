using TMPro;

namespace Core.Components.UiRelated.Windows.Information
{
    public class InformationWindowComponent : WindowComponent
    {
        private InformationComponent _informationComponent;
        private TMP_Text _title;
        private TMP_Text _description;

        public InformationWindowComponent(InformationComponent informationComponent)
        {
            _informationComponent = informationComponent;
        }

        public void SetTmpTexts(TMP_Text title, TMP_Text description)
        {
            _title = title;
            _description = description;
        }

        public void SetText()
        {
            _title.text = _informationComponent.Title;
            _description.text = _informationComponent.Description;
        }

        public void UpdateTextInformation(InformationComponent informationComponent)
        {
            _informationComponent = informationComponent;
            SetTmpTexts(_title, _description);
            SetText();
        }
    }
}