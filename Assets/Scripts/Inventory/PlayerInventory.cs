using New.Player;

public static class PlayerInventory
{
    public static InventoryWithSlots inventoryWithSlots
    { 
        get;
        private set;
    }

    public static void SetInventory(InventoryWithSlots inventory)
    {
        inventoryWithSlots = inventory;
    }
}
