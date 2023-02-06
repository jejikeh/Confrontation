using Wooff.ECS.Context;
using Wooff.ECS.Entity;
using Wooff.ECS.System;

namespace Wooff.ECS.World
{

    public interface IWorld : IUpdateable
    {
        IContext<IEntity> EntityContext { get; }
        IContext<ISystem> SystemContext { get; }
    }
}