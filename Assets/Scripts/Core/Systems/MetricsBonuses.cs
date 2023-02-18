using System.Collections.Generic;
using System.Linq;
using Core.Components.CellComponent;
using Core.Components.MetricBonusComponent;
using Core.Entities.MetricsKeeper;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class MetricsBonuses : IMonoSystem
    {
        private readonly float _time = 10f;
        private float _timer;
        
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            if (_timer < _time)
            {
                _timer += Time.deltaTime * timeScale;
                return;
            }

            var metricBonus = data.Items.Select(x => x.ContextGet<MetricBonus>()).Where(x => x is not null);
            foreach (var bonus in metricBonus)
                MetricsKeeperManager.GetMetric(bonus.Config.MetricType).AddToMetric(bonus.GetBonusAmount());

            _timer = 0;
        }
    }
}