using System.Collections.Generic;
using Core.Components.Metrics;
using Wooff.ECS.Components;

namespace Core.Components.Magic
{
    public abstract class MagicComponent : IComponent
    {
        public abstract Dictionary<MetricType, float> BonusesMultiplier { get; protected set; }
        public abstract MagicType MagicType { get; protected set; }
    }
}