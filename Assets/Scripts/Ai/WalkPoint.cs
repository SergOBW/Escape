using New.Player.Detector;
using New.Player.Detector.Absctract;
using UnityEngine;


[RequireComponent(typeof(DetectableObject))]
public class WalkPoint : MonoBehaviour
{
    private IDetectableObject _detectableObject;
    public Vector3 my_position
    {
        get => transform.position;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
