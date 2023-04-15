using System.Globalization;
using Core.Components.Metrics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Components.UiRelated.Windows.MetricShower
{
    public class MetricShowerWindowComponent : WindowComponent
    {
        private TMP_Text _move;
        private TMP_Text _gold;
        private TMP_Text _speedCreationUnits;
        private Image _moveColorVisualisation;

        public void SetUnityDependencies(TMP_Text move, TMP_Text gold, TMP_Text speedCreationUnits, Image moveColorVisualisation)
        {
            _move = move;
            _gold = gold;
            _speedCreationUnits = speedCreationUnits;
            _moveColorVisualisation = moveColorVisualisation;
        }
        
        public void UpdatePlayerInformation(MetricHandlerBalanceComponent metricHandler, Color color)
        {
            _move.text = metricHandler.Balance[MetricType.Move].ToString(CultureInfo.InvariantCulture);
            _gold.text = metricHandler.Balance[MetricType.Gold].ToString(CultureInfo.InvariantCulture);
            _speedCreationUnits.text = metricHandler.Balance[MetricType.Units].ToString(CultureInfo.InvariantCulture);
            _moveColorVisualisation.DOColor(color, 1f);
        }
    }
}