using System.Linq;
using Core.Components.InformationComponent;
using Core.Components.Metrics.MetricMinerComponent.MetricMinerManager;
using Core.Components.Properties.PropertyComponent;
using Core.Components.RandomableComponent;
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
            MonoWorld.AttachPrefabToEntity(data.Mesh, Handler);
            handler.MonoObject.GetComponent<MeshCollider>().sharedMesh = data.Mesh.GetComponent<MeshFilter>().sharedMesh;
            
            handler.ContextAdd(new Information(data.InformationConfig, handler));
            handler.ContextAdd(new Randomable(data.RandomableConfig, handler));
            handler.ContextAdd(new Property(null, handler));
            _metricMinerHandler = (MetricMinerHandler)handler.ContextAdd(new MetricMinerHandler(data.MetricMinerHandlerConfig, handler));
        }

        public override void OnRemove()
        {
            MonoWorld.DestroyAllChildren(Handler);
            Handler.ContextRemove(Handler.ContextGet<MetricMinerHandler>());
        }
        
        public Cell ChangeToCellType(CellType cellType)
        {
            if (!Config.PlainCell)
                return default;
            
            Handler.ContextRemove(this);
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