using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.UnityRelated
{
    public class UnityCanvasComponent : IComponent
    {
        public Canvas Canvas = null;

        public UnityCanvasComponent InitCanvas(Canvas canvas)
        {
            Canvas = canvas;
            return this;
        }
    }
}