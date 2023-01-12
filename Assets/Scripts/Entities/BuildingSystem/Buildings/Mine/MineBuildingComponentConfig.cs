using UnityEngine;

namespace Entities.BuildingSystem.Buildings.Mine
{
    [CreateAssetMenu(fileName = "MineBuildingConfig", menuName = "BuildingSystem/Buildings/Mine", order = 0)]
    public class MineBuildingComponentConfig : BuildingComponentConfig
    {
        public float GoldPerMinute;
    }
}