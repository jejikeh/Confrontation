using System.Collections.Generic;
using System.Linq;
using Core.Components.CellComponent;
using Core.Entities.MetricsKeeper;
using UnityEngine;
using Wooff.ECS.Contexts;
using Wooff.MonoIntegration;

namespace Core.Systems
{
    public class CellWorldBonus : IMonoSystem
    {
        private readonly float _time = 10f;
        private float _timer;
        
        public void Process(float timeScale, IContext<IMonoEntity, List<IMonoEntity>> data)
        {
            if (_timer < _time)
            {
                _timer += Time.deltaTime * timeScale;
                return;
            }

            var cells = data.Items.Select(x => x.ContextGetAs<Cell>()).Where(x => x is not null);
            foreach (var cell in cells)
                MetricsKeeperManager.GetMetric(cell.Config.MetricTypeBonusTo).AddToMetric(cell.GetBonusAmount());

            _timer = 0;
        }
    }
}