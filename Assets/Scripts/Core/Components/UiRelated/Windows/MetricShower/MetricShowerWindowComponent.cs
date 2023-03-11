using System.Globalization;
using Core.Components.Metrics;
using TMPro;

namespace Core.Components.UiRelated.Windows.MetricShower
{
    public class MetricShowerWindowComponent : WindowComponent
    {
        private TMP_Text _move;
        private TMP_Text _gold;
        private TMP_Text _speedCreationUnits;

        public void SetTexts(TMP_Text move, TMP_Text gold, TMP_Text speedCreationUnits)
        {
            _move = move;
            _gold = gold;
            _speedCreationUnits = speedCreationUnits;
        }
        
        public void UpdateMetrics(MetricHandlerBalance metricHandler)
        {
            _move.text = metricHandler.Balance[MetricType.Move].ToString(CultureInfo.InvariantCulture);
            _gold.text = metricHandler.Balance[MetricType.Gold].ToString(CultureInfo.InvariantCulture);
            _speedCreationUnits.text = metricHandler.Balance[MetricType.SpeedCreationUnits].ToString(CultureInfo.InvariantCulture);
        }
    }
}