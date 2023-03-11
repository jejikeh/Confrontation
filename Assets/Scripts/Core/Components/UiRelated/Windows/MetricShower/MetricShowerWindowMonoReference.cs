using Core.Components.Metrics;
using TMPro;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Components.UiRelated.Windows.MetricShower
{
    public class MetricShowerWindowMonoReference : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _move;
        [SerializeField] 
        private TMP_Text _gold;
        [SerializeField] 
        private TMP_Text _speedCreationUnits;

        private void Start()
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.down;
            GetComponent<MonoEntity>().HandledEntity.ContextGet<MetricShowerWindowComponent>().SetTexts(_move,_gold, _speedCreationUnits);
        }
    }
}