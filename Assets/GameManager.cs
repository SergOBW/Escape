using DefaultNamespace;
using DefaultNamespace.PlayerStates;
using UnityEngine;

public class GameManager : AbstractSingleton<GameManager>
{
    [SerializeField]private string wordGoal;

    public void Win()
    {
        LevelManager.Win();
        GameStateManager.Instance.ChangeState(this,new GameStartState(GameStateManager.Instance));
        Debug.Log("Win");
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
