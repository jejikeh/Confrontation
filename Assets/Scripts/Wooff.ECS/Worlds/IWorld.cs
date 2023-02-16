using System.Collections.Generic;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.ECS.Systems;
using Wooff.Presentation;

namespace Wooff.ECS.Worlds
{
    public interface IWorld<T, T1> 
        where T : IEntity<T>
        where T1 : ISystem<T>
    {
        public IContext<T, List<T>> EntityContext { get; }
        public IContext<T1, HashSet<T1>> SystemContext { get; }
    }
}
