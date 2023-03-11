using System;
using System.Collections.Generic;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.Metrics
{
    [Serializable]
    public class MetricsMinerComponent : IComponent
    {
        [SerializeField]
        public List<MetricType> Mines;
        [SerializeField]
        public float BonusAmount;
    }
}