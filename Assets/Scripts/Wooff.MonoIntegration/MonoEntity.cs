using Core;
using Core.Components;
using Core.Components.CellRelated;
using Core.Components.Tags;
using DG.Tweening;
using UnityEngine;
using Wooff.ECS.Entities;

namespace Wooff.MonoIntegration
{
    public class MonoEntity : MonoBehaviour
    {
        public IEntity HandledEntity { get; set; }
        private float _yPosition;

        private void Start()
        {
            _yPosition = transform.position.y;
        }

        private async void OnMouseEnter()
        {
            if (!HandledEntity.ContextContains<CellTagComponent>())
                return;
            
            transform.DOComplete();
            await transform.DOMoveY(_yPosition + 0.15f, 0.25f).AsyncWaitForCompletion();
            
            var cellMetricUiPanelParentComponent = HandledEntity.ContextGet<CellMetricUiPanelParentComponent>();
            if (cellMetricUiPanelParentComponent is not null || cellMetricUiPanelParentComponent.UiMetricPanel is not null)
                cellMetricUiPanelParentComponent.UiMetricPanel?.GetComponent<TagIconVisualisation>().ToggleVisibility(true);
        }
        
        public async void OnMouseExit()
        {
            if (!HandledEntity.ContextContains<CellTagComponent>())
                return;
            
            transform.DOComplete();
            await transform.DOMoveY(_yPosition, 0.25f).AsyncWaitForCompletion();

            var cellMetricUiPanelParentComponent = HandledEntity.ContextGet<CellMetricUiPanelParentComponent>();
            if (cellMetricUiPanelParentComponent is not null || cellMetricUiPanelParentComponent.UiMetricPanel is not null)
                cellMetricUiPanelParentComponent.UiMetricPanel?.GetComponent<TagIconVisualisation>().ToggleVisibility(false);

        }
    }
}