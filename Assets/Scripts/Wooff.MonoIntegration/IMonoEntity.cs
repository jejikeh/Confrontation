using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;

namespace Wooff.MonoIntegration
{
    public interface IMonoEntity : IEntity<IMonoEntity>
    {
        public T2 ContextGetAs<T2>() where T2 : class, IComponent<IConfig, IMonoEntity>;
        public GameObject MonoObject { get; }
    }
}