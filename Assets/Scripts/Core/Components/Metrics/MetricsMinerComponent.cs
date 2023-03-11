using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Wooff.ECS.Components;

namespace Core.Components.Metrics
{
    [Serializable]
    public class MetricsMinerComponent : IComponent
    {
        // TODO: Delete that
        [FormerlySerializedAs("Mines")] [SerializeField]
        public List<MetricType> Metrics;
        [SerializeField]
        public float BonusAmount;
    }
}