using Core.Interfaces;

namespace Entities.BuildingSystem.Buildings
{
    public interface IBuildingComponent : IConstantStateComponent
    {
        public BuildingComponentConfig BuildingComponentConfig { get; }
    }
}