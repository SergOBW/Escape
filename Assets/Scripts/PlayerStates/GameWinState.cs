using UnityEngine;

namespace DefaultNamespace.PlayerStates
{
    public class GameWinState : IGameState
    {
        private GameStateManager gameStateManager;
        public void Enter()
        {
            Debug.Log("Entering the gameWinState");
        }

        public void Exit()
        {
            Debug.Log("Exit the gameWinState");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                gameStateManager.ChangeState(this,new GameStartState(gameStateManager));
            }
        }

        public void Win()
        {
            
        }

        public void Lose()
        {
            
        }
        public GameWinState(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }
    }
}