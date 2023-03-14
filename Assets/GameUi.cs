using DefaultNamespace.PlayerStates;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    [SerializeField] private GameObject startUi;
    [SerializeField] private GameObject winUi;
    [SerializeField] private GameObject inGameUi;
    private PlayerUi _playerUi;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    public void SetPlayerUi(PlayerUi playerUi)
    {
        _playerUi = playerUi;
    }

    private void OnGameStateChanged(IGameState obj)
    {
        if (obj.GetType() == typeof(GameStartState))
        {
            inGameUi.SetActive(false);
            winUi.SetActive(false);
            startUi.SetActive(true);
        }
        
        if (obj.GetType() == typeof(GamePauseState))
        {
            inGameUi.SetActive(false);
            winUi.SetActive(false);
            startUi.SetActive(true);
        }
        
        if (obj.GetType() == typeof(GamePlayingState))
        {
            inGameUi.SetActive(true);
            winUi.SetActive(false);
            startUi.SetActive(false);
            if (_playerUi != null)
            {
                inGameUi.GetComponent<InGameUi>().Setup(GameManager.Instance.GetLevelGoal(),_playerUi);
            }
            else
            {
                Debug.LogError("There is no playerUi");
            }

        }
        
        if (obj.GetType() == typeof(GameWinState))
        {
            inGameUi.SetActive(false);
            winUi.SetActive(true);
            startUi.SetActive(false);
        }
    }

    public void StartGame()
    {
        var gameStateManager = GameStateManager.Instance;
        //TODO: Sender is not approved - under is bullShit fix that
        GameStateManager.Instance.ChangeState(gameStateManager,new GamePlayingState(gameStateManager));
    }
}
