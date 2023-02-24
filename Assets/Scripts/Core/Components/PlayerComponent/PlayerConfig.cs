using System;
using Core.Components.InformationComponent;
using UnityEngine;
using Wooff.ECS;

namespace Core.Components.PlayerComponent
{
    [Serializable]
    public class PlayerConfig : IConfig
    {
        // TODO: Move visualizeIcon and VisualizeColor to separate component
        public PlayerType PlayerType;
        public InformationConfig InformationConfig;
        public GameObject VisualizeIcon;
        public Color VisualizeColor;
    }
}