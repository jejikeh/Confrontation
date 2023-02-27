using System;
using Core.Components.AudioPlayerComponent;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.InformationComponent;
using Core.Components.Properties.PropertyComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Core.Components.UIComponents.ScreenComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Core.Entities.UI;
using DG.Tweening;
using UnityEngine;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    public class CellPresentation : MonoEntity
    {
        private float _yPosition;
        private void Start()
        {
            _yPosition = transform.position.y;

            var property = ContextGet<Property>();
            var clickable = (Clickable)ContextAdd(new Clickable(
                new ClickableConfig 
                {
                    ClickLayer = ClickLayer.Game
                },
                this));
            
            clickable.OnClick += OnClick;
            property.OnPropertyHandlerAssign += OnPropertyHandlerAssign;
        }

        private void OnPropertyHandlerAssign(object sender, PropertyHandler e)
        {
            Debug.Log($"{e.GetType().FullName} now owner of cell");
        }

        private async void OnClick(object sender, EventArgs e)
        {
            ContextGet<AudioPlayer>().Play("click");
            transform.DOComplete();
            await transform.DOPunchScale(Vector3.up,0.1f).AsyncWaitForCompletion();
            
            if(ScreenPlacer.GetScreenState() == ScreenState.Information)
                (ScreenPlacer.GetWindow(WindowType.Information) as InformationWindow)?.ShowInformation(ContextGet<Information>());
        }

        public async void OnMouseEnter()
        {
            transform.DOComplete();
            await transform.DOMoveY(_yPosition + 0.15f, 0.25f).AsyncWaitForCompletion();
        }

        public async void OnMouseExit()
        {
            transform.DOComplete();
            await transform.DOMoveY(_yPosition, 0.25f).AsyncWaitForCompletion();
        }
    }
}