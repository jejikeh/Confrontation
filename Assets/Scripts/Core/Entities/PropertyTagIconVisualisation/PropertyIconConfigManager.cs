using System.Collections.Generic;
using Core.Components.PlayerComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.PropertyTagIconVisualisation
{
    public class PropertyIconConfigManager : MonoEntity
    {
        [SerializeField] private List<KeyValuePair<GameObject, PlayerType>> _playerIcons =
            new List<KeyValuePair<GameObject, PlayerType>>();

        private void Start()
        {
            
        }
    }
}