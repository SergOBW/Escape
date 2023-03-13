using DefaultNamespace;
using DefaultNamespace.PlayerStates;
using UnityEngine;

public class GameManager : AbstractSingleton<GameManager>
{
    [SerializeField]private string wordGoal;
    
    private void Start()
    {
        LevelManager.Inicialize();
    }

    public void Win()
    {
        GameStateManager.Instance.ChangeState(this,new GameWinState(GameStateManager.Instance));
        Debug.Log("Win");
    }

    public void NextLevel()
    {
        GameStateManager.Instance.ChangeState(this,new GameStartState(GameStateManager.Instance));
        LevelManager.Win();
        Debug.Log("Next Level");
    }
    
    public void Lose()
    {
        LevelManager.Loose();
        GameStateManager.Instance.ChangeState(this,new GameStartState(GameStateManager.Instance));
    }

    public string GetLevelGoal()
    {
        return wordGoal;
    }
}
