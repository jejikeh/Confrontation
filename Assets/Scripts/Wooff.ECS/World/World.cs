using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wooff.ECS.Component;
using Wooff.ECS.Context;
using Wooff.ECS.Entity;
using Wooff.ECS.System;
using NotImplementedException = System.NotImplementedException;

namespace Wooff.ECS.World
{

    public abstract class World<T, T1, T2> : IWorld<T, T1, T2> where T : IEntity<T1> where T1 : IComponent where T2 : IContext<ISystem<T>>
    {
        public abstract IContext<T> EntityContext { get; }
        public abstract T2 SystemContext { get; }

        public async Task Save(string filename)
        {
            await File.WriteAllTextAsync(
                filename,
                JsonConvert.SerializeObject(this,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto, 
                        Formatting = Formatting.Indented,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
        }
        
        public abstract void Initialize();
    }
}