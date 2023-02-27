using Core.Components.InformationComponent;
using Core.Components.Metrics.MetricComponent;
using Core.Components.Metrics.MetricComponent.MetricManager;
using TMPro;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows
{
    public class MetricsWindow : Window
    {
        private TMP_Text _gold;
        private TMP_Text _speedCreationUnits;
        private TMP_Text _movePoints;
        private TMP_Text _playerTitle;
        
        public MetricsWindow(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            _gold = Handler.MonoObject.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            _speedCreationUnits = Handler.MonoObject.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            _movePoints = Handler.MonoObject.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>();
            _playerTitle = Handler.MonoObject.transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
        }

        public void UpdateMetrics(MetricHandler metricHandler)
        {
            _gold.text = metricHandler.GetMetricByType(MetricType.Gold).Amount.ToString();
            _speedCreationUnits.text = metricHandler.GetMetricByType(MetricType.SpeedCreationUnits).Amount.ToString();
            _movePoints.text = metricHandler.GetMetricByType(MetricType.MovePoints).Amount.ToString();
        }

        public void UpdatePlayerInformation(InformationConfig information)
        {
            _playerTitle.text = information.Name;
        }

        public override WindowType WindowType => WindowType.Metrics;
    }
}