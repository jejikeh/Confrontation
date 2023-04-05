using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components
{
    public class PropertyComponent : IComponent
    {
        public IEntity Owner { get; private set; }

        public PropertyComponent(IEntity owner)
        {
            Owner = owner;
        }

        public void GiveOwnershipToEntity(IEntity newOwner, IEntity self)
        {
            Owner = newOwner;
        }

    }
}