using System.Collections.Generic;
using Wooff.ECS.Contexts;

namespace Core.Components.Metrics.MetricMinerComponent.MetricMinerManager
{
    public class MetricMinerContext : 
        Context<IMetricMiner, List<IMetricMiner>>, 
        IListContext<IMetricMiner>
    {
        
    }
}