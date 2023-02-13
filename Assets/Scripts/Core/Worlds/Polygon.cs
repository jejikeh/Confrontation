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
        public override IContext<IMonoEntity> EntityContext => _monoEntityContext;
        private MonoEntityContext _monoEntityContext = new MonoEntityContext();
        public sealed override IContext<ISystem<IMonoEntity>> SystemContext { get; } = new SystemContext<IMonoEntity>();
        
        public override void Initialize()
        {
            SystemContext.Add(new MeshSystem());
            SystemContext.Add(new CameraSystem());
            SystemContext.Add(new SmoothLookAtSystem());
            
            _monoEntityContext.Add<CameraPresentation>();
            foreach (var _ in Enumerable.Range(0,1000))
                _monoEntityContext.Add<BobPresentation>();
        }
    }
}