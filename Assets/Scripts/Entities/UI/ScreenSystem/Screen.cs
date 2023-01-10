using System;
using System.Threading.Tasks;
using DG.Tweening;
using Entities.UI.ScreenSystem.Screens;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entities.UI.ScreenSystem
{
    [RequireComponent(typeof(UIDocument))]
    public class Screen : Entity<IScreenComponent>
    {
        public UIDocument UIDocument;

        private void Start()
        {
            UIDocument = GetComponent<UIDocument>();
        }
        
        protected override async Task OnConstantStateAssign()
        {
            if(ConstantStateComponent is not null)
                await ConstantStateComponent.OnOpen();
        }

        protected override async Task OnConstantStateRemove()
        {
            if(ConstantStateComponent is not null)
                await ConstantStateComponent.OnClose();
        }

        protected void Update()
        {
            UpdateCustomComponents();
        }
    }
}