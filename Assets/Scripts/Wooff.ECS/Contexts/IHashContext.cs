using System.Collections.Generic;

namespace Wooff.ECS.Contexts 
{
    public interface IHashContext<T> : IContext<T, HashSet<T>> where T : IContextItem
    {
    }
}