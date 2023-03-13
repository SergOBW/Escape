using System;
using DefaultNamespace.PlayerStates;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private GameObject startUi;
    [SerializeField] private GameObject winUi;
    [SerializeField] private GameObject inGameUi;

    private void Start()
    {
        gameStateManager.OnGameStateChanged += OnGameStateChanged;
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
        }
        
        if (obj.GetType() == typeof(GameWinState))
        {
            inGameUi.SetActive(false);
            winUi.SetActive(true);
            startUi.SetActive(false);
        }
    }
}
