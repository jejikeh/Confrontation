using TMPro;

namespace Core.Components.UiRelated.Windows
{
    public class InformationWindowComponent : WindowComponent
    {
        private InformationComponent _informationComponent;
        
        public InformationWindowComponent(InformationComponent informationComponent)
        {
            _informationComponent = informationComponent;
        }

        public void SetTmpTexts(TMP_Text title, TMP_Text description)
        {
            title.text = _informationComponent.Title;
            description.text = _informationComponent.Description;
        }
    }
}