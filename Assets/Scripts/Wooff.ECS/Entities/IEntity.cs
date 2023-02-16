using System.Collections.Generic;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;

namespace Wooff.ECS.Entities 
{
    public interface IEntity : IContext<IComponent<IConfig>, HashSet<IComponent<IConfig>>>, IContextItem
    {
        
    }
}