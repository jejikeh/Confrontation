using UnityEngine;

namespace Entities.BuildingSystem.Buildings.Grass
{
    public class GrassBuildingComponent : BuildingComponent<GrassBuildingComponentConfig>
    {
        public GrassBuildingComponent(GrassBuildingComponentConfig customComponentConfig) : base(customComponentConfig)
        {
        }
    }
}