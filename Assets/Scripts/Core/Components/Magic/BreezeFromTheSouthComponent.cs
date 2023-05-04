using System.Collections.Generic;
using Core.Components.Metrics;

namespace Core.Components.Magic
{
    public class BreezeFromTheSouthComponent : MagicComponent
    {
        public override Dictionary<MetricType, float> BonusesMultiplier { get; protected set; } =
            new Dictionary<MetricType, float>()
            {
                { MetricType.Move, 0.5f },
            };
        public override MagicType MagicType { get; protected set; } = MagicType.SevereSnowstorm;
    }
}