using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static int levels { get; private set; }
    private const string LEVEL_NAME = "Level_";
    private const string LEVEL_INDEX = "Levels";
    public static Scene currentScene { get; private set; }

    public static void SetDefaults()
    {
        levels = 1;
        PlayerPrefs.SetInt(LEVEL_INDEX,levels);
        SceneManager.LoadScene(LEVEL_NAME + levels);
    }

    public static void Inicialize()
    {
        levels = PlayerPrefs.GetInt(LEVEL_INDEX);
        currentScene = SceneManager.GetActiveScene();
        if (currentScene == SceneManager.GetSceneByName(LEVEL_NAME + levels))
        {
            return;
        }
        SceneManager.LoadScene(LEVEL_NAME + levels);
        Debug.Log("Inicialize");
    }
    
    public static void Loose()
    {
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public static void Win()
    {
        levels++;
        SceneManager.LoadScene(LEVEL_NAME + levels);
        currentScene = SceneManager.GetSceneByName(LEVEL_NAME + levels);
        PlayerPrefs.SetInt(LEVEL_INDEX,levels);
    }
}
