using System.Collections.Generic;
using Wooff.ECS.Contexts;

namespace Core.Components.MetricBonusComponent.MetricBonusManager
{
    public class MetricsContext : Context<IMetricBonus, List<IMetricBonus>>, IListContext<IMetricBonus>
    {
        
    }
}