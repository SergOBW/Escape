using System;

public interface IInventorySlot
{
    bool isFull { get; }
    bool isEmpty { get; }

    IInventoryItem item { get; }
    Type itemType { get; }
    int amount { get; }
    int capacity { get; }
    string slotType { get; set; }

    void SetItem(IInventoryItem item);
    void Clear();
}
