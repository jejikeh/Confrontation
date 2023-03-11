namespace Core
{
    public static class GameStateManager
    {
        private static UiState _uiState = UiState.None;
        private static TurnState _turnState = TurnState.StartTurn;
        
        public static void SetUiState(UiState newState)
        {
            _uiState = newState;
        }
        
        public static void SetTurnState(TurnState turnState)
        {
            _turnState = turnState;
        }
        
        public static UiState GetUiState => _uiState;
        public static TurnState GetTurnState => _turnState;

    }
}