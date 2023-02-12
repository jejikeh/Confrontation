using System.Linq;
using Core.Presentation;
using Core.Systems;
using Wooff.ECS.Context;
using Wooff.ECS.System;
using Wooff.ECS.World;
using Wooff.Presentation;

namespace Core.Worlds
{
    public class Polygon : World<IMonoEntity, IMonoComponent, IContext<ISystem<IMonoEntity>>>
    {
        public sealed override IContext<IMonoEntity> EntityContext { get; } = new MonoEntityContext();
        public sealed override IContext<ISystem<IMonoEntity>> SystemContext { get; } = new SystemContext<IMonoEntity>();
        
        public override void Initialize()
        {
            SystemContext.Add(new HelloWorldUpdatePrint());
            SystemContext.Add(new MeshSystem());
            /*
            foreach (var _ in Enumerable.Range(0,100))
                EntityContext.Add<BobPresentation>();*/
        }
    }
}