using DefaultNamespace;
using DefaultNamespace.PlayerStates;
using UnityEngine;

public class GameStateManager : AbstractSingleton<GameStateManager>
{
    private GameStartState gameStartState;
    private GamePauseState gamePauseState;
    private GamePlayingState gamePlayingState;

    [SerializeField]private Behaviour[] allowedStateChangers;
    
    public IGameState previosGameState { get; private set; }
    public IGameState currentGameState { get; private set; }

    private void Start()
    {
        gamePauseState = new GamePauseState(this);
        gameStartState = new GameStartState(this);
        gamePlayingState = new GamePlayingState(this);
        ChangeState(this,gameStartState);
    }

    private void Update()
    {
        if (currentGameState != null)
        {
            currentGameState.Update();
        }
    }
    public void ChangeState(object sender, IGameState state)
    {
        if (!CanChangeState(sender))
        {
            Debug.LogWarning("Sender is not allowed to change state");
            return;
        }

        if (currentGameState != null)
        {
            previosGameState = currentGameState;
            previosGameState.Exit();
        }

        currentGameState = state;
        currentGameState.Enter();
    }

    private bool CanChangeState(object sender)
    {
        bool isAllow = false;
        foreach (var script in allowedStateChangers)
        {
            if (script.GetType() == sender.GetType())
            {
                isAllow = true;
                return isAllow;
            }
        }

        if (sender.GetType() == typeof(IGameState))
        {
            isAllow = true;
            return isAllow;
        }
        
        Debug.Log(sender.GetType());

        return isAllow;
    }
    
}
