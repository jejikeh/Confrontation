using Wooff.ECS.Contexts;

namespace Wooff.ECS.Worlds
{
    public interface IWorld
    {
        public EntityContext EntityContext { get; }
        public SystemContext SystemContext { get; }
    }
}
