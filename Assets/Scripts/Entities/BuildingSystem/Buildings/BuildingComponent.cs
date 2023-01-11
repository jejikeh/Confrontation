using Core;
using DG.Tweening;
using UnityEngine;

namespace Entities.BuildingSystem.Buildings
{
    public abstract class BuildingComponent<T> : CustomComponent<T>, IBuildingComponent where T : BuildingComponentConfig
    {
        // TODO: remove Building dependency, after that cache state in BuildingSystemConfig
        protected BuildingComponent(T customComponentConfig) : base(customComponentConfig)
        {
        }

        public BuildingComponentConfig BuildingComponentConfig => ComponentConfig;
    }
}