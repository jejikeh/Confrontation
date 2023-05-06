using JetBrains.Annotations;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.CellRelated
{
    public class CellMetricUiPanelParentComponent : IComponent
    {
        [CanBeNull] public GameObject UiMetricPanel { get; set; }
    }
}