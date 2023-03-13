using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private string wordGoal;

    public void Win()
    {
        //State manager Win
        LevelManager.Win();
    }
    
    public void Lose()
    {
        //State manager Win
        LevelManager.Loose();
    }
}
