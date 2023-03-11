using UnityEngine;
using UnityEngine.UI;

namespace Core.Components.UiRelated.Windows.ToolBox
{
    public class ToolBoxWindowMonoReferenceHelper : MonoBehaviour
    {
        [SerializeField]
        private Button _noneToolButton;
        [SerializeField]
        private Button _informationToolButton;
        [SerializeField]
        private Button _buildToolButton;
        
        private void Start()
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.up;
            _noneToolButton.onClick.AddListener(OnNoneToolButtonClick);
            _informationToolButton.onClick.AddListener(OnInformationButtonClick);
            _buildToolButton.onClick.AddListener(OnBuildToolButtonClick);
        }

        private void OnNoneToolButtonClick()
        {
            GameStateManager.SetUiState(UiState.None);
        }

        private void OnInformationButtonClick()
        {
            GameStateManager.SetUiState(UiState.Information);
        }

        private void OnBuildToolButtonClick()
        {
            GameStateManager.SetUiState(UiState.Build);
        }

        private void OnDestroy()
        {   
            _noneToolButton.onClick.RemoveAllListeners();
            _informationToolButton.onClick.RemoveAllListeners();
            _buildToolButton.onClick.RemoveAllListeners();
        }
    }
}