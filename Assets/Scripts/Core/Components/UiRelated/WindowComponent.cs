using System.Threading.Tasks;
using Core.Components.UnityRelated;
using DG.Tweening;
using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.UiRelated
{
    public class WindowComponentComponent : IComponent, IWindowComponent
    {
        protected GameObject UnitySceneObject;
        
        public WindowComponentComponent(UnityGameObjectComponent gameObjectComponent)
        {
            UnitySceneObject = gameObjectComponent.UnitySceneObject;
            UnitySceneObject.transform.localScale = Vector3.zero;
        }
        
        public async Task OnOpen()
        {
            await UnitySceneObject.transform.DOScale(Vector3.one, 1f).AsyncWaitForCompletion();
        }

        public async Task OnClose()
        {
            await UnitySceneObject.transform.DOScale(Vector3.zero, 1f).AsyncWaitForCompletion();
        }
    }
}