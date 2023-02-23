using System;
using Core.Components.InformationComponent;
using Core.Components.Metrics.MetricMinerComponent.MetricMinerManager;
using Core.Components.RandomableComponent;
using UnityEngine;
using UnityEngine.Serialization;
using Wooff.ECS;

namespace Core.Components.CellComponent
{
    [Serializable]
    public class CellConfig : IConfig
    {
        public GameObject Mesh;
        public InformationConfig InformationConfig;
        public CellType CellType;
        [FormerlySerializedAs("MetricBonusesHandlerConfig")] public MetricMinerHandlerConfig MetricMinerHandlerConfig;
        public RandomableConfig RandomableConfig;
        public bool PlainCell;
    }
}