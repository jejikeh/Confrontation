using System.Collections.Generic;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Systems 
{
    public class SystemContext : Context<ISystem, HashSet<ISystem>>, IHashContext<ISystem>, IProcessable<IContext<IEntity, List<IEntity>>>
    {
        public void Process(float timeScale, IContext<IEntity, List<IEntity>> data)
        {
            foreach(var item in Items)
                item.Process(timeScale, data);
        }
    }
}