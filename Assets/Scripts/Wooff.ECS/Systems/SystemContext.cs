using System.Collections.Generic;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Systems 
{
    public class SystemContext<T, T1> : Context<T, HashSet<T>>, IHashContext<T>, IProcessable<IContext<T1, List<T1>>>, IContextItem 
        where T : ISystem<T1>
        where T1 : IEntity<T1>
    {
        public void Process(float timeScale, IContext<T1, List<T1>> data)
        {
            foreach(var item in Items)
                item.Process(timeScale, data);
        }
    }
}