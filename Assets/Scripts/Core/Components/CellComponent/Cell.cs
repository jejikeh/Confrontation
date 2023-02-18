using Core.Components.CellComponent.Cells;
using Core.Components.MetricBonusComponent;
using Core.Entities.Cells;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.CellComponent
{
    public class Cell : Component<CellConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        IConfig IConfigurable<IConfig>.Config => Config;

        protected Cell(CellConfig data, IMonoEntity handler) : base(data, handler)
        {
            MonoWorld.AttachPrefabToEntity(data.Mesh, Handler);
            handler.MonoObject.GetComponent<MeshCollider>().sharedMesh = data.Mesh.GetComponent<MeshFilter>().sharedMesh;
            handler.ContextAdd(new MetricBonus(data.MetricBonusConfig, handler));
        }

        public override void OnRemove()
        {
            MonoWorld.DestroyAllChildren(Handler);
            Handler.ContextRemove(Handler.ContextGet<MetricBonus>());
        }

        public void ChangeToRandomCell()
        {
            var cellComponent = Handler.ContextGetAs<Cell>();
            Handler.ContextRemove(cellComponent);
            Handler.ContextAdd(RandomCell());
        }

        public Cell RandomCell()
        {
            var cellType = (CellType)Random.Range(2, 5);
            return cellType switch
            {
                CellType.Mine => new Mine(CellManager.GetConfig(CellType.Mine), Handler),
                CellType.Farm => new Farm(CellManager.GetConfig(CellType.Farm), Handler),
                CellType.Stable => new Stable(CellManager.GetConfig(CellType.Stable), Handler),
                _ => this
            };
        }
        
        public static Cell RandomPlainCell(IMonoEntity handler)
        {
            var cellType = (CellType)Random.Range(0, 3);
            return cellType switch
            {
                CellType.Grass => new Grass(CellManager.GetConfig(CellType.Grass), handler),
                CellType.GrassForest => new GrassForest(CellManager.GetConfig(CellType.GrassForest), handler),
                CellType.GrassHill => new GrassHill(CellManager.GetConfig(CellType.GrassHill), handler),
                //CellType.Mine => new Mine(CellManager.GetConfig(CellType.Mine), handler),
                //CellType.Farm => new Farm(CellManager.GetConfig(CellType.Farm), handler),
                //CellType.Stable => new Stable(CellManager.GetConfig(CellType.Stable), handler),
                _ => default
            };
        }
    }
}