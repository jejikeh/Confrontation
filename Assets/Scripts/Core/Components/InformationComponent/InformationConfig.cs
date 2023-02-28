using System;
using UnityEngine;
using Wooff.ECS;

namespace Core.Components.InformationComponent
{
    [Serializable]
    public class InformationConfig : IConfig
    {
        public string Name;
        public string Description;
        public GameObject VisualizeIcon;
    }
}