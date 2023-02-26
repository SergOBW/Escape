using New.Player.Absctract;
using New.Player.Detector.Absctract;
using UnityEngine;

namespace New.Player.Detector
{
    public class DetectableObject : MonoBehaviour, IDetectableObject
    {
        public event ObjectDetectedHandler OnGameObjectDetectedEvent;
        public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;
        
        public void Detected(GameObject detectionSourse)
        {
            OnGameObjectDetectedEvent?.Invoke(detectionSourse, gameObject);
        }

        public void DetectionReleased(GameObject detectionSourse)
        {
            OnGameObjectDetectionReleasedEvent?.Invoke(detectionSourse,gameObject);
        }
        
        public bool HasInventoryItem(out InventoryItemMono component) => gameObject.TryGetComponent(out component);

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        
    }
}