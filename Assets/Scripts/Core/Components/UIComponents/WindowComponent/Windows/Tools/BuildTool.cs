using Core.Components.CellComponent;
using UnityEngine.UI;
using Wooff.ECS;
using Wooff.MonoIntegration;

namespace Core.Components.UIComponents.WindowComponent.Windows.Tools
{
    public class BuildTool : Window
    {
        public override WindowType WindowType => WindowType.BuildTool;
        public CellType SelectedCellType { get; private set; } = CellType.None;
        
        private readonly Button _villageButton;
        private readonly Button _towerButton;
        private readonly Button _fortButton;
        private readonly Button _mineButton;
        
        public BuildTool(IConfig data, IMonoEntity handler) : base(data, handler)
        {
            var monoTransform = Handler.MonoObject.transform.GetChild(0).GetChild(0).GetChild(0);
            _villageButton = monoTransform.GetChild(0).GetComponent<Button>();
            _towerButton = monoTransform.GetChild(1).GetComponent<Button>();
            _fortButton = monoTransform.GetChild(2).GetComponent<Button>();
            _mineButton = monoTransform.GetChild(3).GetComponent<Button>();

            _villageButton.onClick.AddListener(OnVillageButtonClick);
            _towerButton.onClick.AddListener(OnTowerButtonClick);
            _fortButton.onClick.AddListener(OnFortButtonClick);
            _mineButton.onClick.AddListener(OnMineButtonClick);
        }

        // TODO: check naming conversions 
        private void OnVillageButtonClick()
        {
            SelectedCellType = CellType.Village;
        }
        
        private void OnTowerButtonClick()
        {
            SelectedCellType = CellType.TowerOfMagicians;
        }
        
        private void OnFortButtonClick()
        {
            SelectedCellType = CellType.Fort;
        }
        
        private void OnMineButtonClick()
        {
            SelectedCellType = CellType.Mine;
        }

        public override void OnRemove()
        {
            _villageButton.onClick.RemoveAllListeners();
            _towerButton.onClick.RemoveAllListeners();
            _fortButton.onClick.RemoveAllListeners();
            _mineButton.onClick.RemoveAllListeners();
        }
    }
}