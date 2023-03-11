using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.UnityRelated
{
    public class UnityCamera : IComponent
    {
        public Camera Camera;

        public UnityCamera(Camera camera)
        {
            Camera = camera;
        }
    }
}