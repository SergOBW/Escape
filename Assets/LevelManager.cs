using UnityEngine;

public static class LevelManager
{
    public static int levels { get; private set; }
    private const string LEVEL_INDEX = "Levels";
    public static void SetDefaults()
    {
        levels = 0;
        PlayerPrefs.SetInt(LEVEL_INDEX,levels);
        GameSetup.Instance.LoadLevel(levels);
    }

    public static void Inicialize()
    {
        levels = PlayerPrefs.GetInt(LEVEL_INDEX);
        GameSetup.Instance.LoadLevel(levels);
        Debug.Log("Inicialize level " + levels);
    }
    
    public static void Loose()
    {
        Debug.LogWarning("You loose");
        GameSetup.Instance.LoadLevel(levels);
    }
    
    public static void Restart()
    {
        Debug.LogWarning("You restart");
        GameSetup.Instance.LoadLevel(levels);
    }
    public static void Win()
    {
        if (levels + 1 <=  GameSetup.Instance.GetLevelCount())
        {
            levels++;
            PlayerPrefs.SetInt(LEVEL_INDEX,levels);
            GameSetup.Instance.LoadLevel(levels);
        }
        else
        {
            Debug.LogError("You have not create levels");
        }
    }
}
