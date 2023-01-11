using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities.BuildingSystem.Buildings;
using Entities.BuildingSystem.Buildings.Farm;
using Entities.BuildingSystem.Buildings.Grass;
using Entities.BuildingSystem.Buildings.Mine;
using UnityEngine;

namespace Entities.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildSystemConfig", menuName = "BuildingSystem/BuildSystemConfig", order = 0)]
    public class BuildSystemManagerConfig : ScriptableObject
    {
        // TODO: Create class BuildSystemManager like in UIManager or something like that
        public List<BuildingComponentConfig> BuildingComponentConfigs = new List<BuildingComponentConfig>();

        public T FindBuildingConfig<T>() where T : BuildingComponentConfig
        {
            var requiredConfig = BuildingComponentConfigs.FirstOrDefault(x => typeof(T) == x.GetType());
            if (requiredConfig is null)
                throw new Exception("The config was not find in BuildSystemConfig");

            return requiredConfig as T;
        }

        public IBuildingComponent CreateBuildingComponent(BuildingType buildingType)
        {
            var buildingComponentConfig = BuildingComponentConfigs.FirstOrDefault(x => x.Type == buildingType);
            return buildingType switch
            {
                BuildingType.Farm => new FarmBuildingComponent(buildingComponentConfig as FarmBuildingComponentConfig),
                BuildingType.Mine => new MineBuildingComponent(buildingComponentConfig as MineBuildingComponentConfig),
                BuildingType.Grass => new GrassBuildingComponent(
                    buildingComponentConfig as GrassBuildingComponentConfig),
                _ => throw new ArgumentException($"{buildingType} is wrong")
            };
        }
    }
}