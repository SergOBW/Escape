using New.Player.Detector.Absctract;
using UnityEngine;

public delegate void ObjectDetectedHandler(GameObject sourse, GameObject detectedObject);

public interface IDetector
{
    event ObjectDetectedHandler OnGameObjectDetectedEvent;
    event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    void Detect(IDetectableObject detectableObject);
    void Detect(GameObject detectedGameObject);
    void ReleaseDetection(IDetectableObject detectableObject);
    void ReleaseDetection(GameObject detectedGameObject);
}
