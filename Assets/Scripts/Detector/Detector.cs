using System.Collections.Generic;
using System.Linq;
using New.Player.Detector.Absctract;
using UnityEngine;

namespace New.Player.Detector
{
    public class Detector : MonoBehaviour, IDetector
    {
        public event ObjectDetectedHandler OnGameObjectDetectedEvent;
        public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

        public GameObject[] detectedObjects => detectedObjects.ToArray();
        
        protected List<GameObject> _detectedObjects = new List<GameObject>();

        public virtual void Detect(IDetectableObject detectableObject)
        {
            if (!_detectedObjects.Contains(detectableObject.gameObject))
            {
                detectableObject.Detected(this.gameObject);
                _detectedObjects.Add(detectableObject.gameObject);
                OnGameObjectDetectedEvent?.Invoke(gameObject, detectableObject.gameObject);
            }
        }

        public virtual void Detect(GameObject detectedGameObject)
        {
            if (!_detectedObjects.Contains(detectedGameObject))
            {
                _detectedObjects.Add(detectedGameObject);
                
                OnGameObjectDetectedEvent?.Invoke(gameObject, detectedGameObject);
            }
        }

        public virtual void ReleaseDetection(IDetectableObject detectableObject)
        {
            if (_detectedObjects.Contains(detectableObject.gameObject))
            {
                detectableObject.DetectionReleased(gameObject);
                _detectedObjects.Remove(detectableObject.gameObject);
                OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectableObject.gameObject);
            }
        }

        public virtual void ReleaseDetection(GameObject detectedGameObject)
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
            detectedObject = coll.GetComponentInChildren<IDetectableObject>();

            return detectedObject != null;
        }

        protected void RemoveFromList(IDetectableObject detectableObject)
        {
            _detectedObjects.Remove(detectableObject.gameObject);
        }
    }
}