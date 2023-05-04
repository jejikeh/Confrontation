using System;

namespace Core.Components.Metrics
{
    [Flags]
    public enum MetricType
    {
        None = 0,
        Move = 1,
        Gold = 2,
        Units = 4,
        Protection = 8,
        Attack = 16
    }
}