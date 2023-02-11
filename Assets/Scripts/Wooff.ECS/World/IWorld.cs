using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Wooff.ECS.Component;
using Wooff.ECS.Context;
using Wooff.ECS.Entity;
using Wooff.ECS.System;
using Wooff.Presentation;

namespace Wooff.ECS.World
{

    public interface IWorld<T, T1, T2> where T : IEntity<T1> where T1 : IComponent where T2 : IContext<ISystem<T>>

    {
    public IContext<T> EntityContext { get; }
    public T2 SystemContext { get; }

    public Task Save(string fileName);

    public static async Task<T3> Load<T3>(string filename) where T3 : IWorld<T, T1, T2>
    {
        return JsonConvert.DeserializeObject<T3>(await File.ReadAllTextAsync(filename),
            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
    }
    }
}