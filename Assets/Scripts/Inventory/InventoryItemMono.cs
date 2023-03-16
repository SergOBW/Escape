﻿using DefaultNamespace.Touchable;

namespace New.Player.Absctract
{
    using System;
    using New.Player;
    using UnityEngine;

    public class InventoryItemMono : MonoBehaviour, IInventoryItem, ITouchable
    {
        [SerializeField] private InventoryItemInfo _inventoryItemInfoinfo;
        [SerializeField] private Transform slot;
        private InventoryItemState _inventoryItemState;
        public IInventoryItemInfo info { get => _inventoryItemInfoinfo; }
        public IInventoryItemState state { get => _inventoryItemState; }
        public Type type
        {
            get => GetType();
        }
        public IInventoryItem Clone()
        {
            return this;
        }

        public void Use()
        {
            Debug.Log("Use");
        }
        private void Start()
        {
            _inventoryItemState = new InventoryItemState();
            Instantiate(_inventoryItemInfoinfo.prefab, slot);
        }

        public void Interact()
        {
            Debug.Log(gameObject.name + " Interact");
            Destroy(gameObject);
        }
    }
}