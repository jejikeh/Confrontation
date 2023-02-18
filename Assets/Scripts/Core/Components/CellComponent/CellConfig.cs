using System;
using Core.Components.MetricBonusComponent;
using UnityEngine;
using Wooff.ECS;

// TODO: Separate logic to meshes and information and name

namespace Core.Components.CellComponent
{
    [Serializable]
    public class CellConfig : IConfig
    {
        public GameObject Mesh;
        public string Name;
        public string Description;
        public CellType CellType;
        public MetricBonusConfig MetricBonusConfig;
    }
}