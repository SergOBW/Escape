using New.Player.Detector;
using New.Player.Detector.Absctract;
using UnityEngine;

[RequireComponent(typeof(DetectableObject))]
[RequireComponent(typeof(Outline))]
public class DetectableObjectReactionColor : MonoBehaviour
{
    
    private IDetectableObject _detectableObject;
    private Outline _outline;

    private void Awake()
    {
        _detectableObject = GetComponent<IDetectableObject>();
        _outline = GetComponentInChildren<Outline>();
    }
    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectedEvent += OnOnGameObjectDetected;
        _detectableObject.OnGameObjectDetectionReleasedEvent += OnOnGameObjectDetectionReleased;
        _outline.OutlineMode = Outline.Mode.OutlineHidden;
    }
    
    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectedEvent -= OnOnGameObjectDetected;
        _detectableObject.OnGameObjectDetectionReleasedEvent -= OnOnGameObjectDetectionReleased;
        _outline.OutlineMode = Outline.Mode.OutlineHidden;
    }

    private void OnOnGameObjectDetected(GameObject sourse, GameObject detectedobject)
    {
        _outline.OutlineMode = Outline.Mode.OutlineAll;
    }

    private void OnOnGameObjectDetectionReleased(GameObject sourse, GameObject detectedobject)
    {
        _outline.OutlineMode = Outline.Mode.OutlineHidden;
    }
}
