using System.Collections.Generic;
using System.Linq;
using Core.Components.CellRelated;
using Core.Components.Tags;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class UnitPrefabsHandler : Singleton<UnitPrefabsHandler>
    {
        [SerializeField] private List<UnitTagComponentData> _unitComponents;

        public static List<UnitTagComponentData> UnitComponentsGroups()
        { 
            return Instance._unitComponents; 
        }
        
        public static UnitTagComponentData RandomUnitData()
        {
            var cellComponents = UnitComponentsGroups();
            return cellComponents[Random.Range(0, cellComponents.Count)];
        }
    }
}