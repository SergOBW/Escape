using UnityEngine;

namespace DefaultNamespace.PlayerStates
{
    public class GamePauseState : IGameState
    {
        private GameStateManager gameStateManager;
        public void Enter()
        {
            Debug.Log("Enter Pause State");
        }

        public void Exit()
        {
            Debug.Log("Exit Pause State");
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

        public GamePauseState(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }
    }
}