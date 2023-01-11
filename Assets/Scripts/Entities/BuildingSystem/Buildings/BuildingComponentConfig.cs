using Core.Interfaces;
using UnityEngine;

namespace Entities.BuildingSystem.Buildings
{
    public class BuildingComponentConfig : ScriptableObject, ICustomComponentConfig
    {
        public BuildingType Type;
        public GameObject Model;
        public string Title;
        public string Description;
        public int Level = 1;
    }
}