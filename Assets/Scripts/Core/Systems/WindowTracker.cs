using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Components.UiRelated;
using Core.Components.UiRelated.Windows.ChooseCell;
using Core.Components.UiRelated.Windows.Information;
using Core.Components.UiRelated.Windows.MetricShower;
using Core.Components.UiRelated.Windows.ToolBox;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class WindowTracker : Wooff.ECS.Systems.System
    {
        private List<IEntity> _cachedWindows = new List<IEntity>();
        // TODO: cache not the entities count but count from map component|list entity 
        private int _cachedCount;

        public override void UpdateFromEntityContextQuery(float timeScale, EntityContext context)
        {
            foreach (var window in _cachedWindows)
            {
                var windowComponent = window.ContextGetFromInterface<IWindowComponent>();

                if (windowComponent is InformationWindowComponent && GameStateManager.GetUiState != UiState.Information)
                    window.ContextGet<HealthComponent>().Kill();
                
                if (windowComponent is ChooseCellWindowComponent && GameStateManager.GetUiState != UiState.Build)
                    window.ContextGet<HealthComponent>().Kill();
                
                if(GameStateManager.GetUiState == UiState.None && windowComponent is not ToolBoxWindowComponent && windowComponent is not MetricShowerWindowComponent)
                    window.ContextGet<HealthComponent>().Kill();
            }
            
            if (_cachedCount == context.CountInterface<IWindowComponent>()) 
                return;
            
            InitializeGameObjectComponent(context);
            
            _cachedCount = context.CountInterface<IWindowComponent>();
        }

        private void InitializeGameObjectComponent(EntityContext context)
        {
            var newWindows = context
                .ContextWhereQuery(x => x.ContextContains<IWindowComponent>())
                .Where(x => !_cachedWindows.Contains(x))
                .ToArray();

            _cachedWindows.AddRange(newWindows);            
        }
    }
}