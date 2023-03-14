using System;
using DefaultNamespace;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerPrefab;
    [SerializeField] private GameObject roomGameObject;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemySetupPrefab;
    [SerializeField] private GameObject gameUi;
    [SerializeField] private GameObject palayerUi;
    [SerializeField] private GameInput gameInput;
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
        var level = Instantiate(roomGameObject);
        if (hasEnemy)
        {
            Instantiate(enemySetupPrefab);
        }
        var playerUiGo = Instantiate(palayerUi);
        var playerUiScript = playerUiGo.GetComponent<PlayerUi>();
        var _gameInput = Instantiate(gameInput);
        playerUiScript.SetGameInput(_gameInput);
        _gameInput.SetJoystick(playerUiGo.GetComponentInChildren<FloatingJoystick>());
        var spawnPoint = level.GetComponent<LevelPlayerSpawnPoint>().playerSpawnPoint;
        var player = Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
        player.GetComponent<Player>().SetGameInput(_gameInput);
        player.GetComponent<PlayerInventory>().SetPlayerUi(playerUiScript);
        player.GetComponentInChildren<CameraController>().SetGameInput(_gameInput);
        var _gameUi = Instantiate(gameUi);
        _gameUi.GetComponent<GameUi>().SetPlayerUi(playerUiScript);
    }
}
