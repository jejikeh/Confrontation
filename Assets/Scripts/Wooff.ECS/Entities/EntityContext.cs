using System.Collections.Generic;
using Wooff.ECS.Contexts;

namespace Wooff.ECS.Entities 
{
    public class EntityContext : Context<IEntity, List<IEntity>>, IListContext<IEntity> 
    {
        
    }
}