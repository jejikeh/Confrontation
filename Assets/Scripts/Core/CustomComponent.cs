using Core.Interfaces;

namespace Core
{
    public class CustomComponent<T> : CustomComponent where T : ICustomComponentConfig
    {
        protected readonly T ComponentConfig;
        
        protected CustomComponent(T customComponentConfig)
        {
            ComponentConfig = customComponentConfig;
        }
    }
    
    public class CustomComponent : ICustomComponent
    {
        public bool Enabled { get; private set; } = true;
        
        public void Update(float timeScale)
        {
            if(Enabled)
                OnUpdate(timeScale);
        }
        
        public void Enable()
        {
            Enabled = true;
            OnEnable();
        }

        public void Disable()
        {
            Enabled = false;
            OnDisable();
        }

        public void Destroy()
        {
            Disable();
            OnDestroy();
        }
        
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void OnDestroy() { }
        protected virtual void OnUpdate(float timeScale) { }
    }    
}