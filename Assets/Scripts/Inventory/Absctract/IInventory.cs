using System;

public interface IInventory
{
    int capacity { get; set; }
    bool isFull { get; }
    
    string inventoryType { get; set; }

    IInventoryItem GetItem(Type itemType);
    IInventoryItem[] GetAllItems();
    IInventoryItem[] GetAllItems(Type itemType);
    IInventoryItem[] GetEquippedItem();
    int GetItemAmount(Type itemType);

    bool TryToAdd(object sender, IInventoryItem item);
    void Remove(object sender, Type itemType, int amount = 1);
    bool HasItem(Type type, out IInventoryItem item);

}
