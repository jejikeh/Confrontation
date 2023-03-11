using System.Linq;
using Core.Components.CellRelated;
using Core.Components.Metrics;
using Core.Components.Players;
using Core.Components.Tags;
using Core.Components.Tags.UiTags.Windows;
using Core.Components.UnityRelated;
using UnityEngine;
using UnityEngine.UI;
using Wooff.ECS.Entities;
using Wooff.MonoIntegration;

namespace Core.Components.UiRelated.Windows.ChooseCell
{
    public class ChooseCellWindowMonoReference : MonoBehaviour
    {
        [SerializeField] private Button _village;
        [SerializeField] private Button _tower;
        [SerializeField] private Button _fort;
        [SerializeField] private Button _mine;
        
        private void Start()
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.down;
            
            _village.onClick.AddListener(OnVillageClick);
            _tower.onClick.AddListener(OnTowerClick);
            _fort.onClick.AddListener(OnFortClick);
            _mine.onClick.AddListener(OnMineClick);
        }

        private void OnVillageClick()
        {
            ReplaceCell(CellType.Village);
        }
        
        private void OnTowerClick()
        {
            ReplaceCell(CellType.TowerOfMagicians);
        }
        
        private void OnFortClick()
        {
            ReplaceCell(CellType.Fort);
        }
        
        private void OnMineClick()
        {
            ReplaceCell(CellType.Mine);
        }
        
        private void ReplaceCell(CellType cellType)
        {
            var handledEntity = GetComponent<MonoEntity>().HandledEntity;
            var chooseCellWindowComponent = handledEntity.ContextGet<ChooseCellWindowComponent>();
            var player = chooseCellWindowComponent.EntityContext.ContextGetAllFromMap(typeof(PlayerComponent)).FirstOrDefault(x => x.ContextGet<PlayerComponent>().PlayerType == PlayerType.User);

            if (handledEntity.ContextGet<ChooseCellWindowComponent>().ClickedEntity is null || !handledEntity.ContextGet<ChooseCellWindowComponent>().ClickedEntity.ContextGet<CellComponent>().Plain)
                return;

            var playerBalance = player.ContextGet<MetricHandlerBalance>();
            if (playerBalance.Balance[MetricType.Gold] <= 0)
                return;

            if (playerBalance.Balance[MetricType.Move] <= 0)
                return;

            if (!player.ContextGet<PlayerComponent>().Turn)
                return;
            
            if (chooseCellWindowComponent.EntityContext.Count<PropertyComponent>() > 0)
            {
                var allPropertyCells = chooseCellWindowComponent.EntityContext.ContextWhereQuery(x =>
                        x.ContextContains<PropertyComponent>())
                    .Where(p => p.ContextGet<PropertyComponent>().Owner.ContextGet<PlayerComponent>().PlayerType == PlayerType.User)
                    .Select(x => x.ContextGet<UnityGameObjectComponent>());

                if (allPropertyCells.Any())
                {
                    var cellPosition = handledEntity.ContextGet<ChooseCellWindowComponent>().ClickedEntity
                        .ContextGet<UnityGameObjectComponent>().UnitySceneObject.transform.position;
                    if (!allPropertyCells.Any(x =>
                            Vector3.Distance(
                                cellPosition,
                                x.UnitySceneObject.transform.position) <
                            player.ContextGet<PlayerComponent>().MaxBuildDistance))
                        return;
                }
            }
            
            player.ContextGet<MetricHandlerBalance>().RemoveFromMetric(MetricType.Gold, 1);
            player.ContextGet<MetricHandlerBalance>().RemoveFromMetric(MetricType.Move, 1);

            var unityObject = handledEntity.ContextGet<ChooseCellWindowComponent>().ClickedEntity
                .ContextGet<UnityGameObjectComponent>();
            var startPosition = unityObject.StartPosition;
            var startRotation = unityObject.StartRotation;

            chooseCellWindowComponent.EntityContext.ContextAdd(
                new CellTagComponent(CellPrefabsHandler
                        .GetCellComponent(cellType), startPosition, startRotation)
                    .CreateCellEntityContainerAsProperty(player));

            chooseCellWindowComponent.ClickedEntity.ContextGet<HealthComponent>().Kill();
            handledEntity.ContextGet<ChooseCellWindowComponent>().UpdateClickedCell(null);
        }
    }
}