using System;

namespace New.Player.Absctract
{
    public interface IInventoryItemState
    {
        int amount { get; set; }
        bool isEquipped { get; set; }
        bool isSelected { get; set; }
        
        bool isInHand { get; set; }
        
    }
}