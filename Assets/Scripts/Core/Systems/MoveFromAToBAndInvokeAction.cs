﻿using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.TransformRelated;
using Core.Components.UnityRelated;
using DG.Tweening;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class MoveFromAToBAndInvokeActioj : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedEntities = new List<IEntity>();
        private int _cachedCount;

        public override async void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCount != context.Count<PropertyComponent>())
            {
                var newEntities = context
                    .ContextWhereQuery(x => x.ContextContains<MoveFromAtoBAndCallActionComponent>())
                    .Where(x => !_cachedEntities.Contains(x))
                    .ToArray();
                
                if(!newEntities.Any())
                    return;
                
                _cachedEntities.AddRange(newEntities);
                _cachedCount = context.Count<MoveFromAtoBAndCallActionComponent>();
                
                foreach (var entity in newEntities)
                {
                    var moveFromAtoBComponent = entity.ContextGet<MoveFromAtoBAndCallActionComponent>();
                    var entitySceneObject = entity.ContextGet<UnityGameObjectComponent>().UnitySceneObject;
                    
                    entitySceneObject.transform.position =
                        moveFromAtoBComponent.APoint.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform.position;
                    
                    await entitySceneObject.transform
                        .DOMove(
                            moveFromAtoBComponent.BPoint.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform.position, 
                            5f)
                        .AsyncWaitForCompletion();
                    
                    entity.ContextGet<HealthComponent>().Kill();
                }
            }
        }
    }
}