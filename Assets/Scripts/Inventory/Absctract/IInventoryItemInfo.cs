using UnityEngine;

namespace New.Player.Absctract
{
    public interface IInventoryItemInfo
    {
        int maxItemsInventorySlot { get; }
        Sprite spriteIcon { get; }
        GameObject prefab { get; }
        Color color { get; }
        char letter { get; }
        
        string title { get; }
    }
}