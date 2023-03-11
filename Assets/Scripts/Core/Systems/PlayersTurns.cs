using System.Collections.Generic;
using System.Linq;
using Core.Components.Players;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class TurnPlayerComponents : Wooff.ECS.Systems.System
    {
        private IEntity[] _cachedEntities = new IEntity[]{};

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            _cachedEntities = context
                .ContextGetAllFromMap(typeof(PlayerComponent))
                .ToArray();
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            
        }
    }
}