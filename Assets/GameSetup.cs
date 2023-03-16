using DefaultNamespace;
using UnityEngine;

public class GameSetup : AbstractSingleton<GameSetup>
{
    [SerializeField] private GameManager gameManagerPrefab;
    [SerializeField] private GameObject[] roomGameObject;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemySetupPrefab;
    [SerializeField] private GameObject gameUi;
    [SerializeField] private GameObject palayerUi;
    [SerializeField] private GameInput gameInputPrefab;
    private GameObject currentLevel;
    private GameObject currentPlayer;
    private GameInput gameInput;
    private PlayerUi playerUiScript;
    private GameManager gameManager;
    
    public bool hasEnemy;

    private void Awake()
    {
        var go = Instantiate(gameManagerPrefab);
        gameManager = go.GetComponent<GameManager>();
    }

    private void Start()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        var playerUiGo = Instantiate(palayerUi);
        playerUiScript = playerUiGo.GetComponent<PlayerUi>();
        var gameInputGo = Instantiate(gameInputPrefab);
        gameInput = gameInputGo.GetComponent<GameInput>();
        playerUiScript.SetGameInput(gameInput);
        gameInput.SetJoystick(playerUiGo.GetComponentInChildren<FloatingJoystick>());
        var _gameUi = Instantiate(gameUi);
        _gameUi.GetComponent<GameUi>().SetPlayerUi(playerUiScript);
    }

    private void SetupPlayer()
    {
        currentPlayer.GetComponent<Player>().SetGameInput(gameInput);
        currentPlayer.GetComponent<PlayerInventory>().SetPlayerUi(playerUiScript);
        currentPlayer.GetComponentInChildren<CameraController>().SetGameInput(gameInput);
    }
    
    private void UnSetupPlayer()
    {
        currentPlayer.GetComponent<Player>().RemoveGameInput();
        currentPlayer.GetComponent<PlayerInventory>().RemovePlayerUi();
        currentPlayer.GetComponentInChildren<CameraController>().RemoveGameInput();
    }
    
    public void LoadLevel(int index)
    {
        // Secure check
        Debug.Log("Loading " + index + " level");
        Debug.Log("All " + roomGameObject.Length + " levels");
        if (index >= roomGameObject.Length)
        {
            Debug.Log("No more levels");
            LoadDefaultLevel();
            return;
        }
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            if (currentPlayer != null)
            {
                UnSetupPlayer();
                Destroy(currentPlayer);
            }
        }
        currentLevel = Instantiate(roomGameObject[index]);
        gameManager.SetWordGoal(currentLevel.GetComponent<LevelPlayerSpawnPoint>().wordGoal);
        if (hasEnemy)
        {
            Instantiate(enemySetupPrefab);
        }
        var spawnPoint = currentLevel.GetComponent<LevelPlayerSpawnPoint>().playerSpawnPoint;
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
        SetupPlayer();
    }

    public void LoadDefaultLevel()
    {
        Debug.Log("load default");
        LoadLevel(0);
    }

    public int GetLevelCount()
    {
        return roomGameObject.Length;
    }
}
