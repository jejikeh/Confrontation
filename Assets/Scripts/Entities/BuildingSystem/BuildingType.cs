using System;
using Unity.VisualScripting;

namespace Entities.BuildingSystem
{
    [Serializable]
    public enum BuildingType
    {
        Mine,
        Farm,
        Stable,
        Forge,
        Tower,
        Quarry,
        Workshop,
        Fort,
        Grass,
        Sand
    }
}