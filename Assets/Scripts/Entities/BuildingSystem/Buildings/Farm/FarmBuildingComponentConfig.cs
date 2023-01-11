using UnityEngine;

namespace Entities.BuildingSystem.Buildings.Farm
{
    [CreateAssetMenu(fileName = "FarmBuildingConfig", menuName = "BuildingSystem/Buildings/Farm", order = 0)]
    public class FarmBuildingComponentConfig : BuildingComponentConfig
    {
        public float UnitSpeedCreationBonus;
    }
}