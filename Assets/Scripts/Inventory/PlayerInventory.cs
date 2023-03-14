using System.Collections.Generic;
using New.Player;
using New.Player.Absctract;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InventoryWithSlots _playerInventory;
    [SerializeField]private int _capacity;
    [SerializeField]private InventoryType inventoryType;
    private PlayerDetector _detector;
    private PlayerUi _playerUi;
    private Dictionary<char, int> itemGoal;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        GoalInitialize();
    }

    public void SetPlayerUi(PlayerUi playerUi)
    {
        _playerUi = playerUi;
        _playerUi.OnItemTouched += PlayerUiOnItemTouched;
    }

    private void Initialize()
    {
        _playerInventory = new InventoryWithSlots(_capacity, inventoryType);
        Debug.Log(_playerInventory.capacity);
        _detector = GetComponentInChildren<PlayerDetector>();
        //_detector.OnInventoryItemPickedUp += OnInventoryItemPickedUp;
    }

    private void GoalInitialize()
    {
        var goal = GameManager.Instance.GetLevelGoal();
        itemGoal = new Dictionary<char, int>();
        foreach (var goalChar in goal)
        {
            if (itemGoal.ContainsKey(goalChar))
            {
                itemGoal[goalChar]++;
            }
            else
            {
                itemGoal.Add(goalChar,1);
            }
        }

        foreach (var key in itemGoal)
        {
            Debug.Log("Key = " + key.Key + " Value = " + key.Value);
        }
    }

    private void PlayerUiOnItemTouched(InventoryItemMono obj)
    {
        _playerInventory.TryToAdd(this, obj);
        if (itemGoal.ContainsKey(obj.info.letter))
        {
            var letter = obj.info.letter;
            itemGoal[letter]--;
            if (itemGoal[letter] == 0)
            {
                itemGoal.Remove(letter);
            }
        }
        
        foreach (var key in itemGoal)
        {
            Debug.Log("Key = " + key.Key + " Value = " + key.Value);
        }

        if (itemGoal.Count <= 0)
        {
            GameManager.Instance.Win();
        }
    }

    private void OnInventoryItemPickedUp(InventoryItemMono obj)
    {
        _playerInventory.TryToAdd(this, obj);
    }
}
