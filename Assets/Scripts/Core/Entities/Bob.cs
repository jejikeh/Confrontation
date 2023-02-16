using System.Collections.Generic;
using Core.Components.InformationComponent;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.Presentation;

namespace Core.Entities
{
    public class Bob : Context<IComponent<IConfig, IMonoEntity>, HashSet<IComponent<IConfig, IMonoEntity>>>, IMonoEntity
    {
        public Bob(string name, string description)
        {
            ContextAdd(
                new Information(
                    new InformationData()
                    {
                        Description = description,
                        Name = name
                    }, this));
        }
        
        public GameObject MonoObject { get; set; }
    }
}