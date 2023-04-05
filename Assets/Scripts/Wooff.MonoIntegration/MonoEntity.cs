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
            if (!HandledEntity.ContextContains<HoverableTag>())
                return;
            
            transform.DOComplete();
            await transform.DOMoveY(_yPosition + 0.15f, 0.25f).AsyncWaitForCompletion();
        }
        
        public async void OnMouseExit()
        {
            if (!HandledEntity.ContextContains<HoverableTag>())
                return;
            
            transform.DOComplete();
            await transform.DOMoveY(_yPosition, 0.25f).AsyncWaitForCompletion();
        }
    }
}