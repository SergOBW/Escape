using DefaultNamespace;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Transform player;
    [SerializeField]
    private float rotateSpeed = 1.0f;
    private float _rotationVelocity;

    private Transform orientation;

    private float yRotation;
    private void LateUpdate()
    {
        Rotating();
    }

    private void Rotating()
    {
        _rotationVelocity = _gameInput.GetMovementVectorLook().x * rotateSpeed * Time.deltaTime ;
        player.transform.Rotate(Vector3.up * _rotationVelocity);
    }
}
