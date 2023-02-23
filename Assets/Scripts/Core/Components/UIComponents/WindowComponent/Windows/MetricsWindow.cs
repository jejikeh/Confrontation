using Core.Components.Metrics.MetricComponent;
using TMPro;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows
{
    public class MetricsWindow : Window
    {
        private TMP_Text _gold;
        private TMP_Text _speedCreationUnits;
        
        public MetricsWindow(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            _gold = Handler.MonoObject.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            _speedCreationUnits = Handler.MonoObject.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();
        }

        public void UpdateMetrics(Metric gold, Metric speedCreationUnits)
        {
            _gold.text = gold.Amount.ToString();
            _speedCreationUnits.text = speedCreationUnits.Amount.ToString();
        }

        public override WindowType WindowType => WindowType.Metrics;
    }
}