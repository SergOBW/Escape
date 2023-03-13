namespace DefaultNamespace.PlayerStates
{
    public class GamePlayingState : IGameState
    {
        public GameStateManager gameStateManager;
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

        public GamePlayingState(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }
    }
}