using System;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Systems 
{
    public interface ISystem : IProcessable<IContext<IEntity>>
    {
        public void StartFromEntityContextQuery(EntityContext context);
        public void StartFromEntityQuery(IContextQueryable<IEntity> context);
        public void StartFromEntityContext(IContext<IEntity> context);

        public void UpdateFromEntityContextQuery(float timeScale, EntityContext context);
        public void UpdateFromEntityQuery(float timeScale, IContextQueryable<IEntity> context);
        public void UpdateFromEntityContext(float timeScale, IContext<IEntity> context);
    }
}