using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.Players;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class PlayerTagVisualisation : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedEntities = new List<IEntity>();
        private int _cachedCount;

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCount != context.Count<PropertyComponent>())
            {
                var newEntities = context
                    .ContextWhereQuery(x => x.ContextContains<PropertyComponent>())
                    .Where(x => !_cachedEntities.Contains(x))
                    .ToArray();
                
                if(!newEntities.Any())
                    return;
                
                _cachedEntities.AddRange(newEntities);
                _cachedCount = context.Count<PropertyComponent>();
                
                foreach (var entity in newEntities)
                {
                    var visualizationIcon = entity.ContextGet<InformationComponent>().VisualizationIcon;
                    var gameObject = MonoWorld.Instantiate(visualizationIcon, entity.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform);
                    
                    var owner = entity.ContextGet<PropertyComponent>().Owner;
                    if (owner.ContextContains<PlayerComponent>())
                        gameObject.GetComponent<TagIconVisualisation>().SetColor(owner.ContextGet<PlayerComponent>().Color);
                    
                    gameObject.transform.localPosition += Vector3.up;
                }
            }
        }
    }
}