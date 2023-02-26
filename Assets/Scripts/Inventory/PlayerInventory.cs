using New.Player;
using New.Player.Absctract;
using New.Player.Detector;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InventoryWithSlots _playerInventory;
    [SerializeField]private int _capacity;
    [SerializeField]private InventoryType inventoryType;
    private PlayerDetector _detector;
    
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _playerInventory = new InventoryWithSlots(_capacity, inventoryType);
        Debug.Log(_playerInventory.capacity);
        _detector = GetComponentInChildren<PlayerDetector>();
        _detector.OnInventoryItemPickedUp += OnInventoryItemPickedUp;
    }

    private void OnInventoryItemPickedUp(InventoryItemMono obj)
    {
        _playerInventory.TryToAdd(this, obj);
    }
}
