using System;
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
            MetricHandlerBalanceComponent = new MetricHandlerBalanceComponent();
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