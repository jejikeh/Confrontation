using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wooff.MonoIntegration;

namespace Core.Components.UiRelated.Windows.ToolBox
{
    public class InformationWindowMonoReferenceHelper : MonoBehaviour
    {
        [SerializeField] 
        private Button _closeButton;

        [SerializeField] 
        private TMP_Text _title;

        [SerializeField] 
        private TMP_Text _description;

        public void Start()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
            GetComponent<MonoEntity>().HandledEntity.ContextGet<Information.InformationWindowComponent>().SetTmpTexts(_title, _description);
        }

        private void OnCloseButtonClick()
        {
            GetComponent<MonoEntity>().HandledEntity.ContextGet<HealthComponent>().Damage(1);
        }
    }
}