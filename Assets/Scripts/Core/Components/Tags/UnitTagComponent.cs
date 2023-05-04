using System;
using System.Collections.Generic;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags
{
    public class UnitTagComponent : IComponent
    {
        public UnityGameObjectComponent UnityGameObjectComponent;
        public SmoothTranslateComponent SmoothTranslateComponent;
        public MetricHandlerBalanceComponent MetricHandlerBalanceComponent;
        public PropertyComponent PropertyComponent;
        
        public UnitTagComponent(UnitTagComponent unitTagComponent, IEntity handler)
        {
            UnityGameObjectComponent = unitTagComponent.UnityGameObjectComponent;
            SmoothTranslateComponent = unitTagComponent.SmoothTranslateComponent;
            MetricHandlerBalanceComponent = new MetricHandlerBalanceComponent()
            {
                Balance = new Dictionary<MetricType, float>()
                {
                    { MetricType.Protection, 1f },
                    { MetricType.Attack, 1f }
                }
            };
            PropertyComponent = new PropertyComponent(handler);
        }
    }

    [Serializable]
    public class UnitTagComponentData
    {
        [Header("Unit Component Group")]
        public UnityGameObjectComponent UnityGameObjectComponent;
        public SmoothTranslateComponent SmoothTranslateComponent;
    }
}