using UnityEngine;
using Wooff.ECS.Entities;

namespace Wooff.Presentation
{
    public interface IMonoEntity : IEntity<IMonoEntity>
    {
        public GameObject MonoObject { get; set; }
    }
}