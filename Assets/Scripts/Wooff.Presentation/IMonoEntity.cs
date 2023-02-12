using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Entity;

namespace Wooff.Presentation
{
    public interface IMonoEntity : IEntity<IMonoComponent>, IUpdateable
    {
        public GameObject MonoObject { get; set; }
    }
}