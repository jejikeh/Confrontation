using System.Collections.Generic;
using System.Linq;
using Core.Components.MetricBonusComponent.MetricBonusManager;
using Core.Entities.MetricsKeeper;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class MetricsBonuses : IMonoSystem
    {
        private const float Time = 10f;
        private float _timer;
        
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            if (_timer < Time)
            {
                _timer += UnityEngine.Time.deltaTime * timeScale;
                return;
            }

            var metricBonus = data.Items.Select(x => x.ContextGet<MetricBonusesHandler>()).Where(x => x is not null);
            foreach (var bonus in metricBonus.Select(x => x.Items))
                foreach (var metric in bonus)
                    MetricsKeeperManager.GetMetric(metric.MetricType).AddToMetric(metric.GetBonusAmount());

            _timer = 0;
        }
    }
}