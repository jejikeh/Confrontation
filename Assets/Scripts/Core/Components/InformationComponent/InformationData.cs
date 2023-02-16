using System;
using Wooff.ECS;

namespace Core.Components.InformationComponent
{
    [Serializable]
    public class InformationData : IConfig
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}