using System.Threading.Tasks;
using UnityEngine;

namespace Entities.BuildingSystem.Buildings.Mine
{
    public class MineBuildingComponent : BuildingComponent<MineBuildingComponentConfig>
    {
        public MineBuildingComponent(MineBuildingComponentConfig customComponentConfig) : base(customComponentConfig)
        {
        }
    }
}