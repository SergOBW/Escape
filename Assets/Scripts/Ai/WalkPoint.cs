using System;
using New.Player.Detector;
using New.Player.Detector.Absctract;
using UnityEngine;

public enum WayPointStatus
{
    None,
    Near,
    Step
}


[RequireComponent(typeof(DetectableObject))]
public class WalkPoint : MonoBehaviour
{
    private IDetectableObject _detectableObject;
    private Vector3 _myPosition;
    public WayPointStatus wayPointStatus;
    public Vector3 my_position
    {
        get => _myPosition;
    }

    private void Awake()
    {
        _myPosition =transform.position;
        wayPointStatus = WayPointStatus.None;
    }
    
}
