using System;
using System.Collections.Generic;
using System.Linq;
using New.Player.Absctract;
using New.Player.Detector.Absctract;
using UnityEngine;

namespace New.Player.Detector
{
    public class Detector : MonoBehaviour, IDetector
    {
        public event ObjectDetectedHandler OnGameObjectDetectedEvent;
        public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

        public event Action<InventoryItemMono> OnInventoryItemPickedUp;

        public GameObject[] detectedObjects => detectedObjects.ToArray();
        
        private List<GameObject> _detectedObjects = new List<GameObject>();

        public void Detect(IDetectableObject detectableObject)
        {
            if (!_detectedObjects.Contains(detectableObject.gameObject))
            {
                detectableObject.Detected(this.gameObject);
                _detectedObjects.Add(detectableObject.gameObject);
                OnGameObjectDetectedEvent?.Invoke(gameObject, detectableObject.gameObject);
            }
        }

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

        public void Detect(GameObject detectedGameObject)
        {
            if (!_detectedObjects.Contains(detectedGameObject))
            {
                _detectedObjects.Add(detectedGameObject);
                
                OnGameObjectDetectedEvent?.Invoke(gameObject, detectedGameObject);
            }
        }

        public void ReleaseDetection(IDetectableObject detectableObject)
        {
            if (_detectedObjects.Contains(detectableObject.gameObject))
            {
                detectableObject.DetectionReleased(gameObject);
                _detectedObjects.Remove(detectableObject.gameObject);
                OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectableObject.gameObject);
            }
        }

        public void ReleaseDetection(GameObject detectedGameObject)
        {
            if (_detectedObjects.Contains(detectedGameObject))
            {
                _detectedObjects.Remove(detectedGameObject);
                OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectedGameObject);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (isColliderDetectableObject(other, out var detectedObject))
            {
                Detect(detectedObject);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (isColliderDetectableObject(other, out var detectedObject))
            {
                ReleaseDetection(detectedObject);
            }
        }

        private bool isColliderDetectableObject(Collider coll, out IDetectableObject detectedObject)
        {
            detectedObject = coll.GetComponent<IDetectableObject>();

            return detectedObject != null;
        }
    }
}