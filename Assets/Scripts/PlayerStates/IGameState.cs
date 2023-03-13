namespace DefaultNamespace.PlayerStates
{
    public interface IGameState
    {
        public void Enter();
        
        public void Exit();

        public void Update();
        
        public void Win();
        
        public void Lose();
        
    }
}