using System;
using New.Player.Absctract;
public interface IInventoryItem
{
    IInventoryItemInfo info { get; }
    
    IInventoryItemState state { get; }
    Type type { get; }
    IInventoryItem Clone();
    void Use();
}

