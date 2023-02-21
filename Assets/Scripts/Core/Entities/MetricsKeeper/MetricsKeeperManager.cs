using System.Linq;
using Core.Components.MetricComponent;
using Core.Components.MetricComponent.Metrics;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.MetricsKeeper
{
    public class MetricsKeeperManager : StaticMonoEntity<MetricsKeeperManager>
    {
        private void Start()
        {
            ContextAdd(new GoldMetric(new MetricConfig { MetricType = MetricType.Gold}, this));
            ContextAdd(new SpeedCreationUnitsMetric(new MetricConfig { MetricType = MetricType.SpeedCreationUnits}, this));
        }

        // TODO: REMOVE TEST TIMER
        private readonly float _time = 10f;
        private float _timer;
        private void Update()
        {
            if (_timer < _time)
            {
                _timer += Time.deltaTime;
                return;
            }
            
            Debug.Log("Gold: " + GetMetric(MetricType.Gold).Amount);            
            Debug.Log("SpeedCreationUnits: " + GetMetric(MetricType.SpeedCreationUnits).Amount);
            
            _timer = 0;
        }

        public static Metric GetMetric(MetricType metricType)
        {
            return (Metric)Instance.Items.FirstOrDefault(x => ((Metric)x).Config.MetricType == metricType);
        }
    }
}