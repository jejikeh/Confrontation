using System.Threading.Tasks;
using Core.Components.CellComponent;
using Core.Components.ClickableComponent;
using Core.Components.ClickComponent;
using Core.Components.Metrics.MetricComponent;
using Core.Components.Properties.PropertyOwnerComponent;
using Core.Entities;
using Core.Entities.Camera;
using Wooff.MonoIntegration;

namespace Core.Components.PlayerComponent.Players
{
    public class Computer : Player
    {
        public Computer(PlayerConfig data, IMonoEntity handler) : base(data, handler)
        {
        }

        public override async Task OnTurn()
        {
            var cellWorldCreator = StaticMonoWorldFinder.FindCellWorldCreator();
            while (MetricHandler.GetMetricByType(MetricType.MovePoints).Amount > 0)
            {
                await Task.Delay(1350);
                var randomCell = cellWorldCreator.GetFreeCellForBuy(Handler.ContextGet<PropertyHandler>());
                if(BuyCell(randomCell, Cell.RandomBuildingCellType()))
                    StaticMonoWorldFinder
                        .GetEntity<SmoothCamera>()?
                        .ContextGet<Click>()
                        .StartClick(randomCell.Handler.ContextGet<Clickable>());
                
                await Task.Delay(1000);
            }
        }
    }
}