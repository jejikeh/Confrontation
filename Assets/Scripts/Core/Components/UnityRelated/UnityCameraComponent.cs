using UnityEngine;
using Wooff.ECS.Components;

namespace Core.Components.UnityRelated
{
    public class UnityCameraComponent : IComponent
    {
        public Camera Camera;

        public UnityCameraComponent InitCamera(Camera camera)
        {
            Camera = camera;
            return this;
        }
    }
}