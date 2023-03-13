using UnityEngine;

namespace DefaultNamespace.PlayerStates
{
    public class GameStartState : IGameState
    {
        private GameStateManager gameStateManager;
        public void Enter()
        {
            Debug.Log("Entering the startState");
        }

        public void Exit()
        {
            Debug.Log("Exit the startState");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                gameStateManager.ChangeState(this,new GamePlayingState(gameStateManager));
            }
        }

        public void Win()
        {
            
        }

        public void Lose()
        {
            
        }

        public GameStartState(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }
    }
}