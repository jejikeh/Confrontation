using System;
using Core.Components.UiRelated;
using Core.Components.UnityRelated;
using UnityEngine;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Core.Components.Tags.UiTags
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
                new HealthComponent(1),
                this);
        }
    }

    [Serializable]
    public class WindowTagComponentData
    {
        [Header("Window Component Group")]
        [SerializeField] 
        public UnityGameObjectComponent UnityGameObjectComponent;
    }
}