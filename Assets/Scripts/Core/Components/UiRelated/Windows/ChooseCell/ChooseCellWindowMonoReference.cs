using System;
using Core.Components.CellRelated;
using UnityEngine;
using UnityEngine.UI;
using Wooff.ECS.Entities;
using Wooff.MonoIntegration;

namespace Core.Components.UiRelated.Windows.ChooseCell
{
    public class ChooseCellWindowMonoReference : MonoBehaviour
    {
        private static CellType _userBuyCell = CellType.None;
        
        [SerializeField] private Button _village;
        [SerializeField] private Button _tower;
        [SerializeField] private Button _fort;
        [SerializeField] private Button _mine;
        
        private IEntity _handledEntity;

        public static CellType GetState => _userBuyCell;

        public static void StateHandled()
        {
            _userBuyCell = CellType.None;
        }
        
        private void Start()
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.down;
            
            _village.onClick.AddListener(OnVillageClick);
            _tower.onClick.AddListener(OnTowerClick);
            _fort.onClick.AddListener(OnFortClick);
            _mine.onClick.AddListener(OnMineClick);
            _handledEntity = GetComponent<MonoEntity>().HandledEntity;
        }

        private void OnVillageClick()
        {
            _userBuyCell = CellType.Village;
        }
        
        private void OnTowerClick()
        {
            _userBuyCell = CellType.TowerOfMagicians;
        }
        
        private void OnFortClick()
        {
            _userBuyCell = CellType.Fort;
        }
        
        private void OnMineClick()
        {
            _userBuyCell = CellType.Mine;
        }

        private void OnDestroy()
        {
            StateHandled();
        }
    }
}