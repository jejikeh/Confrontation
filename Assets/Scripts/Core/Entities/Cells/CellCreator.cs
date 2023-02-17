using System;
using Core.Components.CellComponent;
using Core.Components.CellComponent.Cells;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    public class CellCreator : MonoEntity
    {
        private void Start()
        {
            var cellPresentationMine = MonoWorld.SpawnEntity<CellPresentation>();
            cellPresentationMine.ContextAdd(new Mine(CellManager.GetConfig(CellType.Mine), cellPresentationMine));
            
            
            var cellPresentationFarm = MonoWorld.SpawnEntity<CellPresentation>();
            cellPresentationFarm.ContextAdd(new Farm(CellManager.GetConfig(CellType.Farm), cellPresentationFarm));

            var cellPresentationStable = MonoWorld.SpawnEntity<CellPresentation>();
            cellPresentationStable.ContextAdd(new Stable(CellManager.GetConfig(CellType.Stable), cellPresentationStable));
        }
    }
}