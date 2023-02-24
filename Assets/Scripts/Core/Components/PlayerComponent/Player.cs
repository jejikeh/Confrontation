using Core.Components.Metrics.MetricComponent;
using Core.Components.Metrics.MetricComponent.MetricManager;
using Core.Components.Properties.PropertyOwnerComponent;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent
{
    public class Player : Component<PlayerConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>,IPlayer
    {
        public MetricHandler MetricHandler { get; }
        IConfig IConfigurable<IConfig>.Config => Config;

        protected Player(PlayerConfig data, IMonoEntity handler) : base(data, handler)
        {
            Handler.ContextAdd(new PropertyHandler(null, Handler));
            MetricHandler = (MetricHandler)Handler.ContextAdd(new MetricHandler(new MetricHandlerConfig(), Handler));
            
            MetricHandler.ContextAdd(new Metric(new MetricConfig()
            {
                MetricType = MetricType.Gold,
                StartAmount = 1
            }));
            
            MetricHandler.ContextAdd(new Metric(new MetricConfig()
            {
                MetricType = MetricType.SpeedCreationUnits,
                StartAmount = 1
            }));
        }
    }
}