using Core.Components.Metrics.MetricComponent;
using Core.Components.Metrics.MetricComponent.MetricManager;
using Core.Components.Properties.PropertyOwnerComponent;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent
{
    public class Player : Component<IConfig, IMonoEntity>, IPlayer
    {
        public MetricHandler MetricHandler { get; }
        public virtual PlayerType PlayerType => PlayerType.None;

        public Player(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            Handler.ContextAdd(new PropertyHandler(null, Handler));
            MetricHandler = (MetricHandler)Handler.ContextAdd(new MetricHandler(new MetricHandlerConfig(), Handler));
            
            MetricHandler.ContextAdd(new Metric(new MetricConfig()
            {
                MetricType = MetricType.Gold,
                StartAmount = 2
            }));
            
            MetricHandler.ContextAdd(new Metric(new MetricConfig()
            {
                MetricType = MetricType.SpeedCreationUnits,
                StartAmount = 1
            }));
        }
    }
}