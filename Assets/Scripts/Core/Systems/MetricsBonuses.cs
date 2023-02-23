using System.Collections.Generic;
using System.Linq;
using Core.Components.Metrics.MetricMinerComponent.MetricMinerManager;
using Core.Components.PlayerComponent;
using Core.Components.Properties.PropertyComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class MetricsBonuses : IMonoSystem
    {
        private const float Time = 2f;
        private float _timer;
        
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            if (_timer < Time)
            {
                _timer += UnityEngine.Time.deltaTime * timeScale;
                return;
            }

            /*
            var metricBonuses =
                data.Items.Select(x => x.ContextGet<MetricMinerHandler>())
                    .Where(x => x is not null).ToList();

            foreach (var player in players)
                foreach (var metric in metricBonuses.Where(x => x.Handler.Co).SelectMany(bonus => bonus.Items))
                    player.MetricHandler.GetMetricByType(metric.MetricType).AddToMetric(metric.GetBonusAmount());
            */
            
            var players = data.Items
                .Where(x => x.ContextGetAs<Player>() is not null)
                .ToList();

            foreach (var player in players)
            {
                var propertyHandler = player.ContextGet<PropertyHandler>();
                foreach (var property in propertyHandler.Items.Select(x => x as Property))
                {
                    foreach (var metric in player
                                 .ContextGetAs<Player>()
                                 .MetricHandler.Items)
                    {
                        var metricMiner = property?.Handler.ContextGet<MetricMinerHandler>();
                        if(metricMiner == null || !metricMiner.ContainsMetric(metric.MetricType))
                            continue;
                        
                        var bonusAmount = metricMiner.GetMetricMiner(metric.MetricType).GetBonusAmount();
                        metric.AddToMetric(bonusAmount);
                    }
                }
            }
            
            _timer = 0;
        }
    }
}