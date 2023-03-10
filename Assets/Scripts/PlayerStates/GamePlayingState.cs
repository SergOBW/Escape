using UnityEngine;

namespace DefaultNamespace.PlayerStates
{
    public class GamePlayingState : IGameState
    {
        public GameStateManager gameStateManager;
        public void Enter()
        {
            Debug.Log("Enter Playing State");
        }

        public void Exit()
        {
            Debug.Log("Exit Playing State");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                gameStateManager.ChangeState(this,new GameWinState(gameStateManager));
            }
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