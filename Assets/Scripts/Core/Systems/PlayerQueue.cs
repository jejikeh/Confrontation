﻿using System.Collections.Generic;
using System.Linq;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.Tags;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class PlayerQueue : Wooff.ECS.Systems.System
    {
        private Queue<IEntity> _cachedPlayers = new Queue<IEntity>();
        private List<IEntity> _cachedCells = new List<IEntity>();

        public IEntity CurrentTurnPlayer()
        {
            return _cachedPlayers.Peek();
        }

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

                if (newEntities.Any())
                    _cachedCells.AddRange(newEntities);
            }   
            
            var player = _cachedPlayers.Peek().ContextGet<PlayerComponent>();
            if (GameStateManager.GetTurnState == TurnState.StartTurn)
            {
                player.Turn = true;
                _cachedPlayers.Peek().ContextGet<MetricHandlerBalanceComponent>()?.AddToMetric(MetricType.Move, 2);
                GameStateManager.SetTurnState(TurnState.ProcessTurn);
                return;
            }
            
            if (GameStateManager.GetTurnState == TurnState.EndTurn)
            {
                player.Turn = false;    
                _cachedPlayers.Enqueue(_cachedPlayers.Dequeue());
                GameStateManager.SetTurnState(TurnState.StartTurn);
                return;
            }

            if (_cachedPlayers.Peek().ContextGet<MetricHandlerBalanceComponent>().Balance[MetricType.Move] <= 0)
            {
                _cachedPlayers.Peek().ContextGet<MetricHandlerBalanceComponent>().SetMetric(MetricType.Move, 0);
                GameStateManager.SetTurnState(TurnState.EndTurn);
            }
        }
    }
}