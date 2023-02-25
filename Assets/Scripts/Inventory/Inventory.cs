using System;
using System.Collections;
using New.Player;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //private UiInventorySlot[] uiSlots;
    //private HotBar _hotBar;
    private int selectedSlotId;
    [SerializeField] private Transform medSlot;
    [SerializeField] private Transform defaultSlot;
    [SerializeField] private Transform ammoSlot;
    private GameObject currentInHand;
    private InventoryWithSlots inventoryPool;
    [SerializeField]private InventoryItemInfo rifle;
    [SerializeField]private InventoryItemInfo smg;
    [SerializeField]private InventoryItemInfo semiAuto;
    [SerializeField]private InventoryItemInfo pistol;
    [SerializeField]private InventoryItemInfo shotGun;
    [SerializeField]private InventoryItemInfo sniper;
    [SerializeField]private InventoryItemInfo med;
    [SerializeField]private InventoryItemInfo ammo;
    public bool canUseItems;
    //public event Action<bool> isWeaponActive;
    //private AnimatorManager _animatorManager;
    private Animator _animator;
    public IInventoryItem currentItemInHand { get; private set; }
    //private UiInventorySetup _uiInventorySetup;
    //private WeaponManagerOfline _weaponManager;
    void Start()
    {
        inventoryPool = new InventoryWithSlots(30, "Pool");
       // inventoryPoolSetup();
        PlayerInventory.SetInventory(new InventoryWithSlots(30, "Player"));
        //uiSlots = GetComponentsInChildren<UiInventorySlot>();
        //_uiInventorySetup = new UiInventorySetup(PlayerInventory.inventoryWithSlots,uiSlots);
        //_hotBar = GetComponentInChildren<HotBar>();
        //_hotBar.OnSelectedSlotChangedEvent += HotBarOnOnSelectedSlotChanged;
        //_weaponManager = GetComponent<WeaponManagerOfline>();
        //_animatorManager = GetComponentInChildren<AnimatorManager>();
        //_animator = _animatorManager.GetCurrentAnimator();
        //PlayerInventory.inventoryWithSlots.OnInventoryDropped += OnInventoryItemDroped;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (currentInHand != null && canUseItems)
            {
                //var item = uiSlots[selectedSlotId].slot.item;
                //item.Use(GetComponent<PlayerLocal>(), selectedSlotId);
                //uiSlots[selectedSlotId].Refresh();
            }
        }
    }

    public void Pickup()
    {
        _animator.SetTrigger("PickUp");
        StartCoroutine(PickupTime());
    }
    
    IEnumerator PickupTime()
    {
        _animator.SetLayerWeight(1,1);
        yield return new WaitForSeconds(0.6f);
        _animator.SetLayerWeight(1,0);
    }
}
