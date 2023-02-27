using System.Collections.Generic;
using Wooff.ECS.Entities;

namespace Wooff.ECS.Components
{
    public abstract class Component<T, T1> : IComponent<T, T1>
        where T : IConfig where T1 : IEntity<T1>
    {
        public T Config { get; private set; }
        public T1 Handler { get; private set; }
        
        private List<IComponent<IConfig, T1>> _manualAddedComponent = new List<IComponent<IConfig, T1>>();

        protected Component(T data, T1 handler)
        {
            Config = data;
            Handler = handler;
        }

        protected IComponent<IConfig, T1> ManualAddComponentToHandler(IComponent<IConfig, T1> component)
        {
            Handler.ContextAdd(component);
            _manualAddedComponent.Add(component);
            return component;
        }

        protected void RemoveAllManualAddedComponentsToHandler()
        {
            foreach (var component in _manualAddedComponent)
                Handler.ContextRemove(component);
        }   

        public virtual void OnRemove()
        {
            
        }
    }
}