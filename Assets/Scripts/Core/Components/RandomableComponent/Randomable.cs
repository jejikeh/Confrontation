using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.RandomableComponent
{
    public class Randomable : Component<RandomableConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        IConfig IConfigurable<IConfig>.Config => Config;

        public Randomable(RandomableConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        public bool GenerateMe()
        {
            return Random.Range(0, 100) <= Config.Chance;
        }

        public static bool GenerateThis(RandomableConfig randomableConfig)
        {
            return Random.Range(0, 100) <= randomableConfig.Chance;
        }
    }
}