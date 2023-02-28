using System.Collections.Generic;
using System.Linq;
using Core.Components.Metrics.MetricComponent;
using Core.Components.PlayerComponent;
using Core.Components.UIComponents.WindowComponent;
using Core.Components.UIComponents.WindowComponent.Windows;
using Core.Entities.MetricsKeeper;
using Core.Entities.UI;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class TurnToMove : IMonoSystem
    {
        public static Player HisMove { get; private set; }
        private static Queue<Player> _players;
        private bool _isFirstCall = true;

        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            if (_isFirstCall)
            {
                _players = null;
                HisMove = null;
                _isFirstCall = false;
            }

            if (_players is null)
            {
                var tempPlayers = data.Items.FindAll(x => x.GetType() == typeof(PlayerPresentation));
                if (tempPlayers.Count > 0)
                {
                    _players = new Queue<Player>();
                    foreach (var player in tempPlayers.Where(x => x.ContextGetAs<Player>().Config.PlayerType != PlayerType.None))
                        _players.Enqueue(player.ContextGetAs<Player>());
                }
                
                StartMove();
            }
            else
            {
                if (HisMove.MetricHandler.GetMetricByType(MetricType.MovePoints).Amount <= 0)
                    EndMove();
            }
        }

        public static void EndMove()
        {
            _players.Dequeue();
            _players.Enqueue(HisMove);
            StartMove();
        }

        // TODO: move move logic to update
        private static async void StartMove()
        {
            HisMove = _players.Peek();
            HisMove.MetricHandler.GetMetricByType(MetricType.MovePoints).AddToMetric(2);

            var metricsWindow = ScreenPlacer.GetWindow(WindowType.Metrics) as MetricsWindow;
            metricsWindow?.UpdatePlayerInformation(HisMove.Config.InformationConfig);

            await HisMove.OnTurn();
        }
    }
}