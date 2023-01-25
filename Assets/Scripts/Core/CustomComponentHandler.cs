using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    public abstract class CustomComponentHandler<T> : MonoBehaviour, ICustomComponentHandler<ICustomComponent> where T : class, IConstantStateComponent
    {
        public List<ICustomComponent> CustomComponents { get; set; } = new List<ICustomComponent>();
        [CanBeNull] protected T ConstantStateComponent;

        public async Task SetConstantState(T component)
        {
            if (ConstantStateComponent?.GetType() == component?.GetType())
                throw new Exception("This state already set!");
            
            await OnConstantStateRemove();
            
            if(ConstantStateComponent is not null)
                RemoveCustomComponent(ConstantStateComponent);
            
            ConstantStateComponent = (T)AddCustomComponent(component);
            await OnConstantStateAssign();
        }

        public async Task RemoveConstantState()
        {
            await OnConstantStateRemove();
            if(ConstantStateComponent is not null)
                RemoveCustomComponent(ConstantStateComponent);

            ConstantStateComponent = default;
        }
        
        [CanBeNull] public T GetConstantState()
        {
            return ConstantStateComponent;
        }

        protected virtual Task OnConstantStateRemove()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnConstantStateAssign()
        {
            return Task.CompletedTask;
        }
        
        public void RemoveCustomComponent<T1>(T1 component) where T1 : class, ICustomComponent
        {
            var customComponent = CustomComponents.FirstOrDefault(x => x == component);
            if (customComponent is null)
                throw new Exception($"{component.GetType().FullName} is not attached to this entity");
            
            customComponent.Disable();
            customComponent.Destroy();
            CustomComponents.Remove(customComponent);
        }
        
        public ICustomComponent AddCustomComponent(ICustomComponent component)
        {
            var requiredComponent = CustomComponents.FirstOrDefault(x => component.GetType() == x.GetType());
            // TODO: not throw class exception
            if (requiredComponent is not null)
                throw new Exception("The component of this class already attached to this component");
            
            CustomComponents.Add(component);
            component.Enable();
            return component;
        }

        public void RemoveCustomComponent(ICustomComponent component)
        {
            var customComponent = CustomComponents.FirstOrDefault(x => x == component);
            if (customComponent is null)
                throw new Exception($"{component.GetType().FullName} is not attached to this entity");
            
            customComponent.Disable();
            customComponent.Destroy();
            CustomComponents.Remove(customComponent);
        }

        public void RemoveCustomComponent<T1>() where T1 : class, ICustomComponent
        {
            var tempComponent = GetCustomComponent<T1>();
            tempComponent?.Disable();
            tempComponent?.Destroy();
            CustomComponents.Remove(tempComponent);
        }
        
        public T1 GetCustomComponent<T1>() where T1 : class, ICustomComponent
        {
            var requiredComponent = CustomComponents.FirstOrDefault(x => typeof(T1) == x.GetType());
            if (requiredComponent is null)
                throw new Exception("The component of this class doesnt attached to this component");

            return requiredComponent as T1;
        }

        public void UpdateCustomComponents(float timeScale = 1f)
        {
            foreach (var component in CustomComponents)
                component.Update(timeScale);
        }
        
        public void DisableAllComponents()
        {
            foreach (var component in CustomComponents)
                component.Disable();
        }

        public void EnableAllComponents()
        {
            foreach (var component in CustomComponents)
                component.Enable();
        }

        public void DestroyAllComponents()
        {
            foreach (var component in CustomComponents)
                component.Destroy();
        }
        
        public void DeleteAllComponents()
        {
            CustomComponents.Clear();
        }
    }
}