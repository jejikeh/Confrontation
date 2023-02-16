using System.Collections.Generic;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.ECS.Systems;

namespace Wooff.ECS.Worlds
{
    public interface IWorld
    {
        public IContext<IEntity, List<IEntity>> EntityContext { get; }
        public IContext<ISystem, HashSet<ISystem>> SystemContext { get; }
    }
}
