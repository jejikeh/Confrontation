using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.CellComponent
{
    public abstract class Cell : Component<CellConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        IConfig IConfigurable<IConfig>.Config => Config;

        protected Cell(CellConfig data, IMonoEntity handler) : base(data, handler)
        {
            handler.MonoObject.GetComponent<MeshFilter>().mesh = data.Mesh;
            handler.MonoObject.GetComponent<MeshCollider>().sharedMesh = data.Mesh;
        }

        public int GetBonusAmount()
        {
            return Config.Level * Config.BonusAmount;
        }
    }
}