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
    
    public bool hasEnemy;

    private void Awake()
    {
        Instantiate(gameManagerPrefab);
    }

    private void Start()
    {
        SpawnAllGameObjects();
    }

    private void SpawnAllGameObjects()
    {
        currentLevel = Instantiate(roomGameObject[0]);
        if (hasEnemy)
        {
            Instantiate(enemySetupPrefab);
        }
        var playerUiGo = Instantiate(palayerUi);
        playerUiScript = playerUiGo.GetComponent<PlayerUi>();
        var gameInputGo = Instantiate(gameInputPrefab);
        gameInput = gameInputGo.GetComponent<GameInput>();
        playerUiScript.SetGameInput(gameInput);
        gameInput.SetJoystick(playerUiGo.GetComponentInChildren<FloatingJoystick>());
        var spawnPoint = currentLevel.GetComponent<LevelPlayerSpawnPoint>().playerSpawnPoint;
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
        SetupPlayer();
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
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            if (currentPlayer != null)
            {
                Destroy(currentPlayer);
            }
        }
        currentLevel = Instantiate(roomGameObject[index]);
        UnSetupPlayer();
        var spawnPoint = currentLevel.GetComponent<LevelPlayerSpawnPoint>().playerSpawnPoint;
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
        SetupPlayer();
    }

    public int GetLevelCount()
    {
        return roomGameObject.Length;
    }
}
