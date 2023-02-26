using System;
using New.Player.Absctract;
using New.Player.Detector;
using UnityEngine;

public class PlayerDetector : Detector
{
    public event Action<InventoryItemMono> OnInventoryItemPickedUp;
    private void Update()
    {
        if (_detectedObjects.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_detectedObjects[0].TryGetComponent(out DetectableObject detectableObject))
                {
                    if (detectableObject.HasInventoryItem(out InventoryItemMono inventoryItem))
                    {
                        OnInventoryItemPickedUp?.Invoke(inventoryItem);
                        detectableObject.DestroySelf();
                        _detectedObjects.RemoveAt(0);
                        Debug.Log("Taked it");
                    }
                }
            }
        }
    }
}
