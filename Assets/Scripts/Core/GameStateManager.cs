namespace Core
{
    public static class GameStateManager
    {
        private static UiState _uiState = UiState.None;
        
        public static void SetUiState(UiState newState)
        {
            _uiState = newState;
        }
        
        public static UiState GetUiState => _uiState;
    }
}