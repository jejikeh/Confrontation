using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Wooff.ECS.Context;
using Wooff.ECS.Entity;
using Wooff.ECS.System;

namespace Wooff.ECS.World
{

    public abstract class World : IWorld
    {
        public IContext<IEntity> EntityContext { get; } = new EntityContext();
        public IContext<ISystem> SystemContext { get; } = new SystemContext();

        [CanBeNull] private readonly IUpdateable _entityContextUpdate;
        [CanBeNull] private readonly IUpdateable<IContext<IEntity>> _systemContextUpdate;

        protected World()
        {
            _entityContextUpdate = EntityContext as IUpdateable;
            _systemContextUpdate = SystemContext as IUpdateable<IContext<IEntity>>;
        }

        public virtual void Update(float timeScale)
        {
            _entityContextUpdate?.Update(timeScale);
            _systemContextUpdate?.Update(timeScale, EntityContext);
        }

        public async Task UpdateParallelAsync(float timeScale)
        {
            await _entityContextUpdate?.UpdateParallelAsync(timeScale)!;
            await _systemContextUpdate?.UpdateParallelAsync(timeScale, EntityContext)!;
        }
        
        public async Task Save(string filename)
        {
            await File.WriteAllTextAsync(
                filename,
                JsonConvert.SerializeObject(this,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented}));
        }
        
        public static async Task<T> Load<T>(string filename) where T : World
        {
            return JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(filename), 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }
    }
}