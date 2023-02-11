using System.Linq;
using Core.Entities;
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
            foreach (var _ in Enumerable.Range(0,1000))
                EntityContext.Add<BobPresentation>();
        }
    }
}