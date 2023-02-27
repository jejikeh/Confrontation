using System.Linq;
using Core.Components.AudioPlayerComponent;
using Core.Components.InformationComponent;
using Core.Components.Metrics.MetricMinerComponent.MetricMinerManager;
using Core.Components.Properties.PropertyComponent;
using Core.Components.RandomableComponent;
using Core.Entities;
using Core.Entities.Cells;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.CellComponent
{
    public class Cell : Component<CellConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>, ICell
    {
        IConfig IConfigurable<IConfig>.Config => Config;
        private readonly MetricMinerHandler _metricMinerHandler;

        private Cell(CellConfig data, IMonoEntity handler) : base(data, handler)
        {
            StaticMonoWorldFinder.AttachPrefabToEntity(data.Mesh, Handler);
            ManualAddComponentToHandler(new AudioPlayer(data.AudioPlayerConfig, handler));
            ManualAddComponentToHandler(new Information(data.InformationConfig, handler));
            ManualAddComponentToHandler(new Randomable(data.RandomableConfig, handler));
            ManualAddComponentToHandler(new Property(null, handler));
            _metricMinerHandler = 
                (MetricMinerHandler)ManualAddComponentToHandler(new MetricMinerHandler(data.MetricMinerHandlerConfig, handler));
        }

        public override void OnRemove()
        {
            StaticMonoWorldFinder.DestroyAllChildren(Handler);
            Handler.ContextRemove(Handler.ContextGet<MetricMinerHandler>());
        }
        
        public Cell ChangeToCellType(CellType cellType)
        {
            if (!Config.PlainCell)
                return default;
            
            Handler.ContextRemove(this);
            RemoveAllManualAddedComponentsToHandler();
            return (Cell)Handler.ContextAdd(new Cell(CellManager.GetConfig(cellType), Handler));
        }
        
        public static Cell RandomPlainCell(IMonoEntity handler)
        { 
            var plainCells= CellManager.Configs.Where(x => x.PlainCell).ToList();
            while (true)
            {
                var randomCellIndex = Random.Range(0, plainCells.Count - 1);
                if (Randomable.GenerateThis(plainCells[randomCellIndex].RandomableConfig))
                    return new Cell(CellManager.GetConfig(plainCells[randomCellIndex].CellType), handler);
            }
        }
    }
}