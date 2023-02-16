using System.Collections.Generic;

namespace Wooff.ECS.Contexts 
{
    public interface IListContext<T> : IContext<T, List<T>> where T : IContextItem
    {
        
    }
}