using System;
using UnityEngine;
using Wooff.ECS;

namespace Core.Components.RandomableComponent
{
    [Serializable]
    public class RandomableConfig : IConfig
    {
        [Range(0, 100)]
        public int Chance;
    }
}