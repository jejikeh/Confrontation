using System.Collections.Generic;
using System.Linq;
using Core.Components.MetricBonusComponent;
using Core.Components.MetricComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Core.Entities.MetricsKeeper;
using Core.Entities.UI;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class DrawMetricText : IMonoSystem
    {
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            (ScreenPlacer.GetWindow(WindowType.Metrics) as MetricsWindow)?.UpdateMetrics(
                MetricsKeeperManager.GetMetric(MetricType.Gold),
                MetricsKeeperManager.GetMetric(MetricType.SpeedCreationUnits));
        }
    }
}