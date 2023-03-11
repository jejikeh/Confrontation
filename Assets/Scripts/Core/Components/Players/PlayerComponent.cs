using System;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.Players
{
    [Serializable]
    public class PlayerComponent : IComponent
    {
        [SerializeField]
        public PlayerType PlayerType;
        [SerializeField]
        public Color Color;
        [SerializeField] 
        public float MaxBuildDistance;
    }
}