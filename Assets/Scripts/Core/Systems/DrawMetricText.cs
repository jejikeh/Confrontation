using System.Collections.Generic;
using System.Linq;
using Core.Components.Metrics.MetricComponent;
using Core.Components.PlayerComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Core.Entities.MetricsKeeper;
using Core.Entities.UI;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class DrawMetricText : IMonoSystem
    {
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            var player = data.Items.Select(x => x.ContextGetAs<Player>()).Where(x => x is not null)
                .FirstOrDefault(x => x.PlayerType == PlayerType.User);
            
            (ScreenPlacer.GetWindow(WindowType.Metrics) as MetricsWindow)?.UpdateMetrics(
                player?.MetricHandler.GetMetricByType(MetricType.Gold) as Metric, 
                player?.MetricHandler.GetMetricByType(MetricType.SpeedCreationUnits) as Metric);
        }
    }
}