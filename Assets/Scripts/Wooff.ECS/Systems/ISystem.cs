using System.Collections.Generic;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Systems 
{
    public interface ISystem : IProcessable<IContext<IEntity, List<IEntity>>>, IContextItem
    {

    }
}