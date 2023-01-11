using Core;
using Core.Interfaces;

namespace Entities
{
    public class Entity : CustomComponentHandler<IConstantStateComponent>
    {
        
    }
    
    public class Entity<T> : CustomComponentHandler<T> where T : class, IConstantStateComponent
    {
        
    }
}