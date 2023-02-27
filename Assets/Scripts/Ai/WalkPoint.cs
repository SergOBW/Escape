using System;
using New.Player.Detector;
using New.Player.Detector.Absctract;
using UnityEngine;


[RequireComponent(typeof(DetectableObject))]
public class WalkPoint : MonoBehaviour
{
    private IDetectableObject _detectableObject;
    private Vector3 _myPosition;
    public Vector3 my_position
    {
        get => _myPosition;
    }

    private void Awake()
    {
        _myPosition =transform.position;
        Debug.Log(_myPosition);
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
