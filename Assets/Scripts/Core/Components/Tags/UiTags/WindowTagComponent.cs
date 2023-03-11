using System;
using Core.Components.UiRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags
{
    public abstract class WindowTagComponent<T> : IComponent where T : IWindowComponent
    {
        public UnityGameObjectComponent UnityGameObjectComponent;
        public T WindowComponent;

        public IEntity CreateWindowEntityContainer()
        {
            return new Entity(
                UnityGameObjectComponent,
                WindowComponent,
                this);
        }
    }

    [Serializable]
    public abstract class WindowTagComponentData
    {
        [Header("Window Component Group")]
        [SerializeField] public UnityGameObjectComponent UnityGameObjectComponent;
    }
}