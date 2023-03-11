using System.Linq;
using Core.Components.Tags;
using Core.Components.UiRelated;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;

namespace Core.Systems
{
    public class CanvasInitialization : Wooff.ECS.Systems.System
    {
        private WindowContextTagComponent _windowContextTagComponent;
        
        public override void StartFromEntityContextQuery(EntityContext context)
        {
            var canvasEntity = context
                .ContextGetAllFromMap(typeof(WindowContextTagComponent))
                .FirstOrDefault();

            _windowContextTagComponent = canvasEntity.ContextGet<WindowContextTagComponent>();
            _windowContextTagComponent.UnityCanvasComponent.InitCanvas(_windowContextTagComponent.UnityGameObjectComponent.UnitySceneObject.GetComponent<Canvas>());
        }
    }
}