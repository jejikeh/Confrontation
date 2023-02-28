using System;
using Core.Components.Properties.PropertyOwnerComponent;
using JetBrains.Annotations;
using UnityEngine.EventSystems;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Components.Properties.PropertyComponent
{
    public interface IProperty : IContextItem
    {
        public IMonoEntity ComponentHandler { get;}
        public PropertyHandler PropertyHandler { get; }
        public void ChangePropertyHandler(PropertyHandler propertyHandler);
        [CanBeNull] public EventHandler<PropertyHandler> OnPropertyHandlerAssign { get; set; }
    }
}