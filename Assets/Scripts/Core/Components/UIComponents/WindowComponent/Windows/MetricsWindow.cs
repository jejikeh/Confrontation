using Core.Components.MetricComponent;
using TMPro;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows.Metrics
{
    public class MetricsWindow : Window
    {
        private TMP_Text _gold;
        private TMP_Text _speedCreationUnits;
        
        public MetricsWindow(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            _gold = Handler.MonoObject.transform.GetChild(0).GetComponent<TMP_Text>();
            _speedCreationUnits = Handler.MonoObject.transform.GetChild(1).GetComponent<TMP_Text>();
        }

        public void UpdateMetrics(Metric gold, Metric speedCreationUnits)
        {
            _gold.text = $"Gold: {gold.Amount}";
            _speedCreationUnits.text = $"Gold: {speedCreationUnits.Amount}";
        }

        public override WindowType WindowType => WindowType.Metrics;
    }
}