using Wooff.ECS.Context;
using Wooff.ECS.Entity;

namespace Wooff.ECS.System
{
    public class SystemContext : UpdateableContext<ISystem, IContext<IEntity>>
    {
        
    }
}