using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.CellRelated;
using Core.Components.Metrics;
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
        private List<IEntity> _cachedCells = new List<IEntity>();
        private int _cachedCount;

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            if (_cachedCount != context.Count<PropertyComponent>())
            {
                var newCellProperties = context
                    .ContextWhereQuery(x => x.ContextContains<PropertyComponent>())
                    .Where(x => !_cachedCells.Contains(x))
                    .ToArray();
                
                if(!newCellProperties.Any())
                    return;
                
                _cachedCells.AddRange(newCellProperties);
                _cachedCount = context.Count<PropertyComponent>();
                
                foreach (var cellEntity in newCellProperties)
                {
                    if (!cellEntity.ContextContains<InformationComponent>())
                        return;
                    
                    var visualizationIcon = cellEntity.ContextGet<InformationComponent>().VisualizationIcon;
                    var gameObject = MonoWorld.Instantiate(visualizationIcon, cellEntity.ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform);
                    cellEntity.ContextGet<CellMetricUiPanelParentComponent>().UiMetricPanel = gameObject;
                    
                    var owner = cellEntity.ContextGet<PropertyComponent>().Owner;
                    if (owner.ContextContains<PlayerComponent>())
                        gameObject.GetComponent<TagIconVisualisation>().SetProperties(owner.ContextGet<PlayerComponent>().Color, cellEntity.ContextGet<MetricHandlerBalanceComponent>());
                    
                    gameObject.transform.localPosition += Vector3.up;
                }
            }
        }
    }
}