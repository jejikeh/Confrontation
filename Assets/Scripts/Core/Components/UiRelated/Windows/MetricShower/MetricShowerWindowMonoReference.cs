using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        private Image _colorVisualisation;

        private void Start()
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.down;
            GetComponent<MonoEntity>().HandledEntity.ContextGet<MetricShowerWindowComponent>()?.SetUnityDependencies(_move,_gold, _colorVisualisation);
        }
    }
}