using System.Linq;
using System.Threading.Tasks;
using Core.Components.AudioPlayerComponent;
using Core.Components.CellComponent;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.Metrics.MetricComponent;
using Core.Components.Metrics.MetricComponent.MetricManager;
using Core.Components.Properties.PropertyComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Core.Entities;
using Core.Entities.Camera;
using Core.Entities.Cells;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent
{
    public class Player : Component<PlayerConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>,IPlayer
    {
        public MetricHandler MetricHandler { get; }
        IConfig IConfigurable<IConfig>.Config => Config;

        private PropertyHandler _propertyHandler;

        protected Player(PlayerConfig data, IMonoEntity handler) : base(data, handler)
        {
            _propertyHandler = (PropertyHandler)ManualAddComponentToHandler(new PropertyHandler(null, Handler));
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
            
            MetricHandler.ContextAdd(new Metric(new MetricConfig()
            {
                MetricType = MetricType.MovePoints,
                StartAmount = 2
            }));
        }

        public override void OnRemove()
        {
            RemoveAllManualAddedComponentsToHandler();
        }

        public bool BuyCell(Cell cell, CellType changeToCellType)
        {
            if (!cell.Config.PlainCell || changeToCellType == CellType.None)
                return false;
            
            var properties = _propertyHandler.Items.Select(x => x.ComponentHandler.ContextGet<Cell>());
            if (!properties.Any(property => 
                    Vector3.Distance(property.Handler.MonoObject.transform.position, cell.Handler.MonoObject.transform.position) < CellManager.MaxBuildDistance))
                return false;
            
            _propertyHandler.ContextAdd(
                cell
                    .ChangeToCellType(changeToCellType)
                    .Handler
                    .ContextGet<Property>());
            
            MetricHandler.GetMetricByType(MetricType.MovePoints).AddToMetric(-1);
            MetricHandler.GetMetricByType(MetricType.Gold).AddToMetric(-10);
            
            cell.Handler.ContextGet<AudioPlayer>().Play("click");
            return true;
        }

        public virtual Task OnTurn()
        {
            /*
            var playerCapital = _propertyHandler.Items.FirstOrDefault(x => x.ComponentHandler.ContextGet<Cell>().Config.CellType == CellType.City);
            StaticMonoWorldFinder
                .GetEntity<SmoothCamera>()?
                .ContextGet<Click>()
                .StartClick(playerCapital?.ComponentHandler.ContextGet<Clickable>());*/
                
            return Task.CompletedTask;
        }
    }
}
