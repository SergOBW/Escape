using UnityEngine;

namespace New.Player.Absctract
{
    public interface IInventoryItemInfo
    {
        string id { get; }
        int maxItemsInventorySlot { get; }
        string title { get; }
        string description { get; }
        Sprite spriteIcon { get; }
        GameObject prefab { get; }
        
        GameObject probe { get; }
    }
}