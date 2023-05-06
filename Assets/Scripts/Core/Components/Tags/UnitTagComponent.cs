using System;
using System.Collections.Generic;
using Core.Components.Metrics;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Components.Tags
{
    public class UnitTagComponent : IComponent
    {
        public UnityGameObjectComponent UnityGameObjectComponent;
        public MetricHandlerBalanceComponent MetricHandlerBalanceComponent;
        
        public UnitTagComponent(UnitTagComponentData unitTagComponent, Vector3 position, Quaternion rotation)
        {
            UnityGameObjectComponent = new UnityGameObjectComponent(unitTagComponent.UnityGameObjectComponent)
            {
                StartRotation = rotation,
                StartPosition = position
            };
            MetricHandlerBalanceComponent = new MetricHandlerBalanceComponent()
            {
                Balance = new Dictionary<MetricType, float>()
                {
                    { MetricType.Protection, 1f },
                    { MetricType.Attack, 1f }
                }
            };
        }
        
        public IEntity CreateTagEntityContainerMovingFromAtoB(IEntity fromA, IEntity toB, Action<IEntity, IEntity, int, EntityContext> action)
        {
            return new Entity(
                UnityGameObjectComponent,
                MetricHandlerBalanceComponent,
                new MoveFromAtoBAndCallActionComponent(fromA, toB, action),
                new HealthComponent(10f),
                this);
        }
    }

    [Serializable]
    public class UnitTagComponentData
    {
        [Header("Unit Component Group")]
        public UnityGameObjectComponent UnityGameObjectComponent;
    }
}