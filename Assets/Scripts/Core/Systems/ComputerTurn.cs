using System.Collections.Generic;
using System.Linq;
using Core.Components.Players;
using Core.Components.Tags;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class ComputerTurn : Wooff.ECS.Systems.System
    {
        private Queue<IEntity> _cachedPlayers = new Queue<IEntity>();
        private List<IEntity> _cachedCells = new List<IEntity>();

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var players = context
                .ContextGetAllFromMap(typeof(PlayerComponent));

            foreach (var player in players)
                _cachedPlayers.Enqueue(player);
        }

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCells.Count != context.Count<CellTagComponent>())
            {
                var newEntities = context
                    .ContextWhereQuery(x => x.ContextContains<CellTagComponent>())
                    .Where(x => !_cachedCells.Contains(x))
                    .ToArray();
                
                if(!newEntities.Any())
                    return;
                
                _cachedCells.AddRange(newEntities);
            }

            var player = _cachedPlayers.Peek().ContextGet<PlayerComponent>();
            
        }
    }
}