using System;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components
{
    [Serializable]
    public class InformationComponent : IComponent
    {
        public string Title = string.Empty;
        public string Description = string.Empty;
        public GameObject VisualizationIcon;
    }
}