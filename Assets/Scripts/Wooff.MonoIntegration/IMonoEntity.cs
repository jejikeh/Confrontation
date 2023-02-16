using UnityEngine;
using Wooff.ECS.Entities;

namespace Wooff.MonoIntegration
{
    public interface IMonoEntity : IEntity<IMonoEntity>
    {
        public GameObject MonoObject { get; set; }
    }
}