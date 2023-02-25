using System;
using New.Player.Absctract;

namespace New.Player
{
    [Serializable]
    public class InventoryItemState : IInventoryItemState
    {
        public int itemAmount;
        public bool isItemEquipped;
        public bool isItemSelected;
        public bool isItemInHand;
        public int amount { get => itemAmount; set => itemAmount = value; }
        public bool isEquipped {  get => isItemEquipped; set => isItemEquipped = value; }
        public bool isSelected { get => isItemSelected; set => isItemSelected = value; }
        public bool isInHand { get => isItemInHand; set => isItemInHand =  value; }

        public InventoryItemState()
        {
            itemAmount = 0;
            isItemEquipped = false;
            isItemSelected = false;
            isItemInHand = false;
        }
        
        
    }
}