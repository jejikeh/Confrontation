using System.Collections.Generic;
using Wooff.ECS.Contexts;

namespace Core.Components.Metrics.MetricComponent.MetricManager
{
    public class MetricContext : Context<IMetric, HashSet<IMetric>>, IHashContext<IMetric>
    {
        
    }
}