using System.Collections.Generic;
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

        private void Awake()
        {
            SystemContext.ContextAdd(new PrintInformationInfo());
            SystemContext.ContextAdd(new RandomTranslate());
            
            foreach (var monoEntity in FindObjectsByType<MonoEntity>(FindObjectsSortMode.None))
                EntityContext.ContextAdd(monoEntity);
        }

        private void Update()
        {
            (SystemContext as IProcessable<IContext<IMonoEntity, List<IMonoEntity>>>)?.Process(1f, EntityContext);
        }
    }
}
