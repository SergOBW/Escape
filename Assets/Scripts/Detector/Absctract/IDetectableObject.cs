using UnityEngine;

namespace New.Player.Detector.Absctract
{
    public interface IDetectableObject
    {
        event ObjectDetectedHandler OnGameObjectDetectedEvent;
        event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

        GameObject gameObject { get; }

        void Detected(GameObject detectionSourse);
        void DetectionReleased(GameObject detectionSourse);
    }
}