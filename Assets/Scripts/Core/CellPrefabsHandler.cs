using System.Collections.Generic;
using System.Linq;
using Core.Components.CellRelated;
using Core.Components.Tags;
using UnityEngine;

namespace Core
{
    public class CellPrefabsHandler : Singleton<CellPrefabsHandler>
    {
        [SerializeField] private List<CellTagComponentData> _cellComponents;

        public static List<CellTagComponentData> CellComponentsGroups()
        { 
            return Instance._cellComponents; 
        }
        
        public static CellTagComponentData GetCellComponent(CellType cellType)
        { 
            return Instance._cellComponents.FirstOrDefault(x => x.CellComponent.CellType == cellType);
        }

        public static CellTagComponentData RandomPlainCell()
        {
            var plainCells = CellComponentsGroups().Where(x => x.CellComponent.Plain).ToList();
            while (true)
            {
                var randomCellIndex = Random.Range(0, plainCells.Count);
                if (plainCells[randomCellIndex].RandomableComponent.GenerateMe())
                    return plainCells[randomCellIndex];
            }
        }
    }
}