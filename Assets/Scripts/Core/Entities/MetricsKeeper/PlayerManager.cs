using System;
using Core.Components.CellComponent;
using Core.Components.PlayerComponent;
using Core.Components.PlayerComponent.Players;
using Core.Components.Properties.PropertyComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Core.Entities.Cells;
using Wooff.MonoIntegration;

namespace Core.Entities.MetricsKeeper
{
    public class PlayerManager : MonoEntity
    {
        private void Start()
        {
            foreach (var playerType in PlayerConfigManager.PlayerTypes)
            {
                var monoEntity = MonoWorld.SpawnEntity<PlayerPresentation>("Player: "+ playerType);
                switch (playerType)
                {
                    case PlayerType.None:
                        var none = monoEntity.ContextAdd(new None(
                            null, 
                            monoEntity));
                        break;
                    case PlayerType.Computer:
                        var computer = monoEntity.ContextAdd(new Computer(
                            null, 
                            monoEntity));
                        
                        var computerCell = CellWorldCreator.GetRandomCell();
                        computerCell.ChangeToCellType(CellType.City);

                        computer.Handler
                            .ContextGet<PropertyHandler>()
                            .ContextAdd(computerCell.Handler.ContextGet<Property>());
                        
                        break;
                    case PlayerType.User:
                        var user = monoEntity.ContextAdd(new User(
                            null, 
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