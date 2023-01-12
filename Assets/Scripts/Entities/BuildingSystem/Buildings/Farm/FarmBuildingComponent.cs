using UnityEngine;

namespace Entities.BuildingSystem.Buildings.Farm
{
    public class FarmBuildingComponent : BuildingComponent<FarmBuildingComponentConfig>
    {
        public FarmBuildingComponent(FarmBuildingComponentConfig customComponentConfig) : base(customComponentConfig)
        {
        }
    }
}