using System;
using UnityEngine;
using Wooff.ECS.Components;
using Random = UnityEngine.Random;

namespace Core.Components
{
    [Serializable]
    public class RandomableComponent : IComponent
    {
        [Range(0, 100)]
        public int Chance;

        public bool GenerateMe()
        {
            return Random.Range(0, 100) <= Chance;
        }
    }
}
