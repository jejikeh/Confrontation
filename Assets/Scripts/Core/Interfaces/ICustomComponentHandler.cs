using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core.Interfaces
{
    public interface ICustomComponentHandler<T> where T : class, ICustomComponent
    {
        public List<T> CustomComponents { get; set; }
        public T AddCustomComponent(T component);
        public void RemoveCustomComponent(T component);
        public void RemoveCustomComponent<T1>() where T1 : class, T;
        [CanBeNull] public T1 GetCustomComponent<T1>() where T1 : class, T;
        public void UpdateCustomComponents(float timeScale);
    }
}