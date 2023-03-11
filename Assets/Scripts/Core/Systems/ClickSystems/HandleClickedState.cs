using System.Linq;
using Core.Components;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems.ClickSystems
{
    public abstract class HandleClickedState<T> : Wooff.ECS.Systems.System where T : class, IComponent
    {
        private IEntity[] _cachedEntities;
        private int _cachedCount;

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCount != context.Count<T>())
            {
                _cachedEntities = context.ContextWhereQuery(x =>
                    x.ContextContains<T>()).ToArray();

                _cachedCount = context.Count<T>();
            }
            
            foreach (var clickedEntities in _cachedEntities)
            {
                var clickableComponent = clickedEntities.ContextGet<ClickableComponent>();
                if(!clickableComponent.Clicked)
                    continue;
                
                ProcessClickedEntity(context, clickedEntities);
            }
        }

        protected abstract void ProcessClickedEntity(EntityContext context, IEntity clickedEntity);
    }
}