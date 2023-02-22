using System;
using Core.Components.InformationComponent;
using Core.Components.MetricBonusComponent;
using Core.Components.MetricBonusComponent.MetricBonusManager;
using Core.Components.RandomableComponent;
using UnityEngine;
using UnityEngine.Serialization;
using Wooff.ECS;

// TODO: Separate logic to meshes and information and name

namespace Core.Components.CellComponent
{
    [Serializable]
    public class CellConfig : IConfig
    {
        public GameObject Mesh;
        public InformationConfig InformationConfig;
        public CellType CellType;
        public MetricBonusesHandlerConfig MetricBonusesHandlerConfig;
        public RandomableConfig RandomableConfig;
        public bool PlainCell;
    }
}