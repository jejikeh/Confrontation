using System.Collections.Generic;
using System.Linq;
using Core.Components.CellComponent;
using Core.Components.WorldCellComponent.Cells;
using UnityEngine;

namespace Core.Entities.Cells
{
    /// <summary>
    /// stores all configuration and information about cells
    /// </summary>
    public class CellManager : Singleton<CellManager>
    {
        [SerializeField] private List<CellConfig> _configs = new List<CellConfig>();
        [SerializeField] private WorldCellsConfig _worldCellsConfig;
        [SerializeField] private GameObject _cellPresentationPrefab;

        public static List<CellConfig> Configs => Instance._configs;
        public static WorldCellsConfig WorldCellsConfig => Instance._worldCellsConfig;
        public static GameObject CellPresentationPrefab => Instance._cellPresentationPrefab;
        public static float MaxBuildDistance = 2f;

        public static CellConfig GetConfig(CellType cellType)
        {
            return Instance._configs.FirstOrDefault(x => x.CellType == cellType);
        }
    }
}