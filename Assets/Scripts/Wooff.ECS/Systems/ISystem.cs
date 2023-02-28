using System.Collections.Generic;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Systems 
{
    public interface ISystem<T> : IProcessable<IContext<T, List<T>>>, IContextItem 
        where T : IContextItem, IEntity<T>
    {

    }
}