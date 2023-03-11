using Core.Components.Metrics;
using TMPro;

namespace Core.Components.UiRelated.Windows.MetricShower
{
    public class MetricShowerWindowComponent : WindowComponent
    {
        private TMP_Text _gold;
        private TMP_Text _speedCreationUnits;

        public void SetTexts(TMP_Text gold, TMP_Text speedCreationUnits)
        {
            _gold = gold;
            _speedCreationUnits = speedCreationUnits;
        }
        
        public void UpdateMetrics(MetricHandlerBalance metricHandler)
        {
            _gold.text = metricHandler.Balance[MetricType.Gold].ToString();
            _speedCreationUnits.text = metricHandler.Balance[MetricType.SpeedCreationUnits].ToString();
        }
    }
}