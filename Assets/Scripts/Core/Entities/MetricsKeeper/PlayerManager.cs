﻿using System;
using System.Collections.Generic;
using Core.Components.CellComponent;
using Core.Components.PlayerComponent;
using Core.Components.PlayerComponent.Players;
using Core.Components.Properties.PropertyComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Core.Entities.Cells;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.MetricsKeeper
{
    public class PlayerManager : MonoEntity
    {
        [SerializeField] private List<PlayerConfig> _players = new List<PlayerConfig>();

        private void Start()
        {
            foreach (var playerConfig in _players)
            {
                var monoEntity = MonoWorld.SpawnEntity<PlayerPresentation>("Player: "+ playerConfig);
                switch (playerConfig.PlayerType)
                {
                    case PlayerType.None:
                        var none = monoEntity.ContextAdd(new None(
                            playerConfig, 
                            monoEntity));
                        break;
                    case PlayerType.Computer:
                        var computer = monoEntity.ContextAdd(new Computer(
                            playerConfig, 
                            monoEntity));
                        var computerCell = CellWorldCreator.GetRandomCell();
                        computerCell.ChangeToCellType(CellType.City);
                        computer.Handler
                            .ContextGet<PropertyHandler>()
                            .ContextAdd(computerCell.Handler.ContextGet<Property>());
                        break;
                    case PlayerType.User:
                        var user = monoEntity.ContextAdd(new User(
                            playerConfig, 
                            monoEntity));
                        
                        var userCell = CellWorldCreator.GetRandomCell();
                        userCell.ChangeToCellType(CellType.City);
                        
                        user.Handler
                            .ContextGet<PropertyHandler>()
                            .ContextAdd(userCell.Handler.ContextGet<Property>());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}