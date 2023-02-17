using System;
using Core.Components.MetricComponent;
using UnityEngine;
using UnityEngine.Serialization;
using Wooff.ECS;

// TODO: Separate logic boost to metrics

namespace Core.Components.CellComponent
{
    [Serializable]
    public class CellConfig : IConfig
    {
        public Mesh Mesh;
        public string Name;
        public string Description;
        public int Level;
        public int BonusAmount;
        public CellType CellType;
        [FormerlySerializedAs("MetricTypeBonusTo")] public MetricType MetricTypeBonusTo;
    }
}