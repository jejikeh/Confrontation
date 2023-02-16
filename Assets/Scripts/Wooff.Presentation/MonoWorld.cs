using System.Collections.Generic;
using Core.Entities;
using Core.Systems;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Contexts;
using Wooff.ECS.Entities;
using Wooff.ECS.Systems;
using Wooff.ECS.Worlds;

namespace Wooff.Presentation
{
    public class MonoWorld : MonoBehaviour, IWorld<IMonoEntity, IMonoSystem>
    {
        public IContext<IMonoEntity, List<IMonoEntity>> EntityContext { get; } = new EntityContext<IMonoEntity>();
        public IContext<IMonoSystem, HashSet<IMonoSystem>> SystemContext { get; } = new SystemContext<IMonoSystem, IMonoEntity>();

        private void Start()
        {
            SystemContext.ContextAdd(new PrintInformationInfo());
            EntityContext.ContextAdd(new Bob("BNBNN", "DJIUOESDJEHFUISREFH"));
        }

        private void Update()
        {
            (SystemContext as IProcessable<IContext<IMonoEntity, List<IMonoEntity>>>)?.Process(1f, EntityContext);
        }
    }
}
