using System;
using Core.Components.CellComponent;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.InformationComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Core.Entities.UI;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class CellPresentation : MonoEntity
    {
        private void Start()
        {
            // TODO: unsub
            var clickable = (Clickable)ContextAdd(new Clickable(
                new ClickableConfig 
                {
                    ClickLayer = ClickLayer.Game
                },
                this));
            
            clickable.OnClick += OnClick;
        }

        private void OnClick(object sender, EventArgs e)
        {
            (ScreenPlacer.GetWindow(WindowType.Information) as InformationWindow)?.ShowInformation(ContextGet<Information>());
        }
    }
}