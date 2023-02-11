using Wooff.ECS.Context;

namespace Wooff.ECS.System
{
    public class SystemContext<T> : UpdateableContext<ISystem<T>, IContext<T>>
    {
        
    }
}