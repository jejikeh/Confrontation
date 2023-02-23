using System.Collections.Generic;
using Core.Components.PlayerComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.MetricsKeeper
{
    public class PlayerConfigManager : StaticMonoEntity<PlayerConfigManager>
    {
        [SerializeField] private List<PlayerType> _players = new List<PlayerType>();
        public static List<PlayerType> PlayerTypes => Instance._players;
    }
}