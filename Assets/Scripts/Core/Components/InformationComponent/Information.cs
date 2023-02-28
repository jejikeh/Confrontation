using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.InformationComponent
{
    public class Information : Component<InformationConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        IConfig IConfigurable<IConfig>.Config => Config;

        public Information(InformationConfig data, IMonoEntity handler) : base(data, handler)
        {
        }
    }
}