using System.Collections.Generic;
using Wooff.ECS.Contexts;

namespace Wooff.ECS.Entities 
{
    public class EntityContext<T> : Context<T, List<T>>, IListContext<T> 
        where T : IEntity<T>
    {
        
    }
}