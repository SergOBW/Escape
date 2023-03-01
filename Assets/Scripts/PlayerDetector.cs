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
                foreach (var detectedObject in _detectedObjects)
                {
                    if (detectedObject.TryGetComponent(out DetectableObject detectableObject))
                    {
                        if (detectableObject.HasInventoryItem(out InventoryItemMono inventoryItem))
                        {
                            OnInventoryItemPickedUp?.Invoke(inventoryItem);
                            RemoveFromList(detectableObject);
                            detectableObject.DestroySelf();
                            Debug.Log("Taked it");
                            break;
                        }
                    }
                }
            }
        }
    }
}
