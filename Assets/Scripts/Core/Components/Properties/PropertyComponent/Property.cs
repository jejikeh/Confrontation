using System;
using Core.Components.Properties.PropertyOwnerComponent;
using JetBrains.Annotations;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.Properties.PropertyComponent
{
    public class Property : 
        Component<IConfig, IMonoEntity>, 
        IProperty
    {
        public PropertyHandler PropertyHandler { get; private set; }
        
        public Property(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            PropertyHandler = null;
        }

        public void ChangePropertyHandler(PropertyHandler propertyHandler)
        {
            if (PropertyHandler == propertyHandler) 
                return;
            
            PropertyHandler = propertyHandler;
            OnPropertyHandlerAssign?.Invoke(this, propertyHandler);

        }

        [CanBeNull] public EventHandler<PropertyHandler> OnPropertyHandlerAssign { get; set; }
    }
}