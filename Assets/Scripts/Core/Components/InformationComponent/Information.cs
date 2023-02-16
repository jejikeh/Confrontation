using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.ECS.Entities;
using Wooff.Presentation;

namespace Core.Components.InformationComponent
{
    public class Information : Component<InformationData, IMonoEntity>,IComponent<InformationData, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        public void WhoAmIm()
        {
            Debug.Log(Config.Description + Config.Name);
        }

        IConfig IConfigurable<IConfig>.Config => Config;

        public Information(InformationData data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}