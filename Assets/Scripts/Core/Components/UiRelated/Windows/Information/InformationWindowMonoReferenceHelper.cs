using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wooff.MonoIntegration;

namespace Core.Components.UiRelated.Windows.Information
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
            var component = GetComponent<MonoEntity>().HandledEntity.ContextGet<InformationWindowComponent>();
            component.SetTmpTexts(_title, _description);
            component.SetText();
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.down;
        }

        private void OnCloseButtonClick()
        {
            GetComponent<MonoEntity>().HandledEntity.ContextGet<HealthComponent>().Kill();
        }
    }
}