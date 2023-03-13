namespace DefaultNamespace.PlayerStates
{
    public class GameStartState : IGameState
    {
        private GameStateManager gameStateManager;
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Win()
        {
            throw new System.NotImplementedException();
        }

        public void Lose()
        {
            throw new System.NotImplementedException();
        }

        public GameStartState(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }
    }
}