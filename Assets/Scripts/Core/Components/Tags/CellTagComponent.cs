using System;
using Core.Components.CellRelated;
using Core.Components.Metrics;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags
{
    public class CellTagComponent : IComponent
    {
        public UnityGameObjectComponent UnityGameObjectComponent;
        public CellComponent CellComponent;
        public ClickableComponent ClickableComponent;
        public InformationComponent InformationComponent;
        public RandomableComponent RandomableComponent;
        public MovePointCameraTagComponent MovePointCameraTagComponent;
        public HoverableTag HoverableTag;
        public HealthComponent HealthComponent;
        public MetricsMinerComponent MetricsMinerComponent;

        public CellTagComponent(CellTagComponentData cellTagComponentData, Vector3 position, Quaternion rotation)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(cellTagComponentData.UnityGameObjectComponent)
                {
                    StartPosition = position,
                    StartRotation = rotation
                };
            
            CellComponent = cellTagComponentData.CellComponent;
            ClickableComponent = new ClickableComponent();
            InformationComponent = cellTagComponentData.InformationComponent;
            RandomableComponent = cellTagComponentData.RandomableComponent;
            MovePointCameraTagComponent = new MovePointCameraTagComponent();
            HoverableTag = new HoverableTag();
            HealthComponent = new HealthComponent(cellTagComponentData.HealthComponent);
            MetricsMinerComponent = cellTagComponentData.MetricsMinerComponent;
        }
        
        public IEntity CreateCellEntityContainer()
        {
            return new Entity(
                UnityGameObjectComponent,
                CellComponent,
                ClickableComponent,
                InformationComponent,
                RandomableComponent,
                MovePointCameraTagComponent,
                HoverableTag,
                HealthComponent,
                MetricsMinerComponent,
                this);
        }

        public IEntity CreateCellEntityContainerAsProperty(IEntity handler)
        {
            return new Entity(
                UnityGameObjectComponent,
                CellComponent,
                ClickableComponent,
                InformationComponent,
                RandomableComponent,
                MovePointCameraTagComponent,
                HoverableTag,
                HealthComponent,
                MetricsMinerComponent,
                new PropertyComponent(handler),
                this);
        }
    }

    [Serializable]
    public class CellTagComponentData
    {
        [Header("Cell Component Group")]
        [SerializeField] public UnityGameObjectComponent UnityGameObjectComponent;
        [SerializeField] public CellComponent CellComponent;
        [SerializeField] public InformationComponent InformationComponent;
        [SerializeField] public RandomableComponent RandomableComponent;
        [SerializeField] public HealthComponent HealthComponent;
        [SerializeField] public MetricsMinerComponent MetricsMinerComponent;
    }
}
