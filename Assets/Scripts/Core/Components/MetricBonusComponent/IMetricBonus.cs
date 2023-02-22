using Core.Components.MetricComponent;
using Wooff.ECS.Contexts;

namespace Core.Components.MetricBonusComponent
{
    public interface IMetricBonus : IContextItem
    {
        public int GetBonusAmount();
        public MetricType MetricType { get; }
    }
}