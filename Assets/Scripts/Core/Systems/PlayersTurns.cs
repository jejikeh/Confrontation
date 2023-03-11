using System.Collections.Generic;
using System.Linq;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.Tags;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class PlayersTurns : Wooff.ECS.Systems.System
    {
        private Queue<IEntity> _cachedEntities = new Queue<IEntity>();
        private bool _isSetToTurn;

        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var players = context
                .ContextGetAllFromMap(typeof(PlayerComponent));

            foreach (var player in players)
                _cachedEntities.Enqueue(player);
        }
        
        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            var player = _cachedEntities.Peek().ContextGet<PlayerComponent>();
            if (GameStateManager.GetTurnState == TurnState.StartTurn && !_isSetToTurn)
            {
                player.Turn = true;
                _isSetToTurn = true;
                _cachedEntities.Peek().ContextGet<MetricHandlerBalance>().AddToMetric(MetricType.Move, 2);
                MetricBalanceMining.CurrentTurnEntity = _cachedEntities.Peek();
            }

            if (GameStateManager.GetTurnState == TurnState.EndTurn && _isSetToTurn)
            {
                player.Turn = false;
                _isSetToTurn = false;
                _cachedEntities.Enqueue(_cachedEntities.Dequeue());
                GameStateManager.SetTurnState(TurnState.StartTurn);
            }
        }
    }
}