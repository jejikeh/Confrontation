using System;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Systems
{
    public abstract class System : ISystem
    {
        public void Start(IContext<IEntity> data)
        {
            if (data is EntityContext entityContext)
                StartFromEntityContextQuery(entityContext);
            
            if (data is IContextQueryable<IEntity> contextQueryable)
                StartFromEntityQuery(contextQueryable);
            
            StartFromEntityContext(data);
        }

        public void Update(float timeScale, IContext<IEntity> data)
        {
            if (data is EntityContext entityContext)
                UpdateFromEntityContextQuery(timeScale, entityContext);
            
            if (data is IContextQueryable<IEntity> contextQueryable)
                UpdateFromEntityQuery(timeScale, contextQueryable);
            
            UpdateFromEntityContext(timeScale, data);
        }

        public virtual void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
        }

        public virtual void UpdateFromEntityQuery(float timeScale, IContextQueryable<IEntity> context)
        {
        }

        public virtual void UpdateFromEntityContext(float timeScale, IContext<IEntity> context)
        {
        }

        public virtual void StartFromEntityContextQuery(EntityContext context)
        {
        }

        public virtual void StartFromEntityQuery(IContextQueryable<IEntity> context)
        {
        }

        public virtual void StartFromEntityContext(IContext<IEntity> context)
        {
        }
    }
}