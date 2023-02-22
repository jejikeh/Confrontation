using System;
using System.Threading.Tasks;
using Core.Components.CellComponent;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.InformationComponent;
using Core.Components.MetricBonusComponent;
using Core.Components.MetricBonusComponent.MetricBonusManager;
using Core.Components.MetricComponent;
using Core.Components.UIComponents.ScreenComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Core.Entities.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Wooff.MonoIntegration;

namespace Core.Entities.Cells
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class CellPresentation : MonoEntity
    {
        private float _yPosition;
        private void Start()
        {
            _yPosition = transform.position.y;
            // TODO: unsub
            var clickable = (Clickable)ContextAdd(new Clickable(
                new ClickableConfig 
                {
                    ClickLayer = ClickLayer.Game
                },
                this));
            
            clickable.OnClick += OnClick;
        }

        private async void OnClick(object sender, EventArgs e)
        {
            transform.DOComplete();
            await transform.DOPunchScale(Vector3.up,0.1f).AsyncWaitForCompletion();
            
            if(ScreenPlacer.GetScreenState() == ScreenState.Information)
                (ScreenPlacer.GetWindow(WindowType.Information) as InformationWindow)?
                    .ShowInformation(ContextGet<Information>());
            else if (ScreenPlacer.GetScreenState() == ScreenState.Build)
            {
                ContextGet<MetricBonusesHandler>().ContextAdd(new MetricBonus(new MetricBonusConfig()
                {
                    BonusAmount = 10,
                    MetricType = MetricType.SpeedCreationUnits
                }));
            }
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