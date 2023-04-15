using System;
using System.Collections.Generic;
using Core.Components.Metrics;
using Core.Components.Players;
using UnityEngine;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags
{
    public class PlayerTagComponent : IComponent
    {
        public MetricHandlerBalanceComponent MetricHandlerBalanceComponent;
        public PlayerComponent PlayerComponent;

        public PlayerTagComponent(PlayerTagComponentData data)
        {
            MetricHandlerBalanceComponent = new MetricHandlerBalanceComponent()
            {
                Balance = new Dictionary<MetricType, float>()
                {
                    { MetricType.Move, 2 },
                    { MetricType.Gold, 2 },
                    { MetricType.Units, 10}
                }
            };
            PlayerComponent = data.PlayerComponent;
        }
        
        public IEntity CreatePlayerTagEntityContainer()
        {
            return new Entity(
                MetricHandlerBalanceComponent,
                PlayerComponent,
                this);
        }
    }

    [Serializable]
    public class PlayerTagComponentData
    {
        [Header("Player Tag Component Group")]
        [SerializeField]
        public PlayerComponent PlayerComponent;
    }
}