using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InventoryType {
    Player,
    NotPlayer
}

namespace New.Player
{
    public class InventoryWithSlots : IInventory
    {
        public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
        public event Action<object, Type, int> OnInventoryItemRemovedEvent;
        public event Action<object> OnInventoryStateChangeEvent;
        public event Action<object, Type, int> OnInventoryDropped;
        
        public event Action<object, Type, int> OnInventoryItemRemoveFromSlotEvent;
        
        public int capacity { get; set; }
        public bool isFull => _slots.All(slot => slot.isFull);
        public InventoryType inventoryType { get; set; }

        private List<IInventorySlot> _slots;

        public InventoryWithSlots(int capacity, InventoryType inventoryType)
        {
            this.capacity = capacity;
            this.inventoryType = inventoryType;

            _slots = new List<IInventorySlot>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                _slots.Add(new InventorySlot());
                _slots[i].slotType = inventoryType;
            }
        }
        
        public IInventoryItem GetItem(Type itemType)
        {
            return _slots.Find(slot => slot.itemType == itemType).item;
        }

        public IInventoryItem[] GetAllItems()
        {
            var allItems = new List<IInventoryItem>();
            foreach (var slot in _slots)
            {
                if (!slot.isEmpty)
                {
                    allItems.Add(slot.item);
                }
            }

            return allItems.ToArray();
        }

        public IInventoryItem[] GetAllItems(Type itemType)
        {
            var allItems = new List<IInventoryItem>();
            var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
            foreach (var slot in slotsOfType)
            {
                allItems.Add(slot.item);
            }

            return allItems.ToArray();
        }

        public IInventoryItem[] GetEquippedItem()
        {
            var requiredSlots = _slots.FindAll(slot => !slot.isEmpty && slot.item.state.isEquipped);
            var equippedItems = new List<IInventoryItem>();
            foreach (var slot in requiredSlots)
            {
                equippedItems.Add(slot.item);
            }

            return equippedItems.ToArray();
        }

        public int GetItemAmount(Type itemType)
        {
            var amount = 0;
            var allItemSlots = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
            foreach (var itemSlot in allItemSlots)
            {
                amount += itemSlot.amount;
            }

            return amount;
        }

        public bool TryToAdd(object sender, IInventoryItem item)
        {
            var slotWithSameItemButNotEmpty =
                _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);

            if (slotWithSameItemButNotEmpty != null)
            {
                return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);
            }

            var emptySlot = _slots.Find(slot => slot.isEmpty);
            if (emptySlot != null)
            {
                //Debug.Log("Try to add to EMPTY" + item.state.amount);
                return TryToAddToSlot(sender, emptySlot, item);
            }
            
            Debug.LogWarning($"Cannot add item ({item.type}), amount: {item.state.amount}, " + $"because there is no place for that.");
            return false;
        }

        public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
        {
            Debug.Log("Item succesful added" + item.info.title);
            var fits = slot.amount + item.state.amount <= item.info.maxItemsInventorySlot;
            var amountToAdd = fits ? item.state.amount : item.info.maxItemsInventorySlot - slot.amount;
            //Debug.Log(amountToAdd + " AMOUNT TO ADD");
            var amountLeft = item.state.amount - amountToAdd;
            //Debug.Log(amountLeft + " LEFT");
            var clonedItem = item.Clone();

            if (slot.isEmpty)
            {
                clonedItem.state.amount = amountToAdd;
                slot.SetItem(clonedItem);
            }
            else
            {
                slot.item.state.amount += amountToAdd;
            }
            
            //Debug.Log($"Item added to inventory slot type = {slot.slotType}. Item type {item.type}, amount {amountToAdd}");
            OnInventoryItemAddedEvent?.Invoke(sender,item,amountToAdd);
            OnInventoryStateChangeEvent?.Invoke(sender);

            if (amountLeft <= 0)
            {
                return true;
            }
            
            item.state.amount = amountLeft;
            return TryToAdd(sender, item);
        }

        public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
        {
            if (fromSlot.isEmpty)
            {
                return;
            }

            if (toSlot.isFull)
            {
                return;
            }

            if (!toSlot.isEmpty && fromSlot.itemType != toSlot.itemType)
            {
                return;
            }

            if (fromSlot.slotType == InventoryType.Player && toSlot.slotType != InventoryType.Player)
            {
                return;
            }

            var slotCopacity = fromSlot.capacity;
            var fits = fromSlot.amount + toSlot.amount <= slotCopacity;
            var amountToAdd = fits ? fromSlot.amount : slotCopacity - toSlot.amount;
            var amountLeft = fromSlot.amount - amountToAdd;

            if (toSlot.isEmpty)
            {
                toSlot.SetItem(fromSlot.item);
                fromSlot.Clear();
                OnInventoryStateChangeEvent?.Invoke(sender);
            }

            toSlot.item.state.amount += amountToAdd;
            if (fits)
            {
                fromSlot.Clear();
            }
            else
            {
                fromSlot.item.state.amount = amountLeft;
                OnInventoryStateChangeEvent?.Invoke(sender);
            }


        }

        public void Remove(object sender, Type itemType, int amount = 1)
        {
            var slotsWithItem = GetAllSlots(itemType);
            if (slotsWithItem.Length == 0)
            {
                return;
            }

            var amountToRemove = amount;
            var count = slotsWithItem.Length;

            for (int i = count - 1; i >= 0; i--)
            {
                var slot = slotsWithItem[i];
                if (slot.amount >= amountToRemove)
                {
                    slot.item.state.amount -= amountToRemove;

                    if (slot.amount <= 0)
                    {
                        slot.Clear();
                    }
                    
                    //Debug.Log($"Item removed from inventory. Item type {itemType}, amount {amountToRemove}");
                    OnInventoryItemRemovedEvent?.Invoke(sender,itemType, amountToRemove);
                    OnInventoryStateChangeEvent?.Invoke(sender);
                    break;
                }

                var amountRemoved = slot.amount;
                amountToRemove -= slot.amount;
                slot.Clear();
                //Debug.Log($"Item removed from inventory. Item type {itemType}, amount {amountRemoved}");
                OnInventoryItemRemovedEvent?.Invoke(sender,itemType, amountRemoved);
                OnInventoryStateChangeEvent?.Invoke(sender);
            }
        }
        
        public void DropFromSlot(object sender, Type itemType, IInventorySlot slot,  int amount)
        {
            if (slot != null && slot.itemType == itemType)
            {
                slot.Clear();
                OnInventoryStateChangeEvent?.Invoke(sender);
                OnInventoryDropped?.Invoke(sender,itemType,amount);
            }
            else
            {
                Debug.LogWarning("Slot is null or ItemType dissmatch");
            }
        }

        public bool HasItem(Type type, out IInventoryItem item)
        {
            item = GetItem(type);
            return item != null;
        }

        public IInventorySlot[] GetAllSlots(Type itemType)
        {
            return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
        }
        
        public IInventorySlot[] GetAllSlots()
        {
            return _slots.ToArray();
        }

        public void RemoveFromSlot(object sender, Type type, IInventorySlot slot, int amount)
        {
            if (slot != null && slot.itemType == type)
            {
                if (slot.amount <= 0)
                {
                    Debug.Log("Empty");
                    return;
                }
                slot.item.state.amount -= amount;

                if (slot.amount <= 0)
                {
                    slot.Clear();
                }
                OnInventoryStateChangeEvent?.Invoke(sender);
                OnInventoryItemRemoveFromSlotEvent?.Invoke(sender,type,amount);
                //Debug.Log("Removed" + type + " " + amount);
            }
            else
            {
                Debug.LogWarning("Slot is null or ItemType dissmatch");
            }
        }

        public void RemoveFromSlotById(object sender, Type type, int id, int amount)
        {
            if (_slots[id] != null && _slots[id].itemType == type)
            {
                var slot = _slots[id];
                if (slot.amount <= 0)
                {
                    Debug.Log("Empty");
                    return;
                }
                slot.item.state.amount -= amount;

                if (slot.amount <= 0)
                {
                    slot.Clear();
                }
                OnInventoryStateChangeEvent?.Invoke(sender);
                OnInventoryItemRemoveFromSlotEvent?.Invoke(sender,type,amount);
                Debug.Log("Removed" + type + " " + amount);
            }
            else
            {
                Debug.LogWarning("Slot is null or ItemType dissmatch");
            }
        }
    }
    
}