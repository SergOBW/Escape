using System;
using DefaultNamespace;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameInput _gameInput;
    [SerializeField] private Transform player;
    [SerializeField]
    private float rotateSpeed = 1.0f;
    private float _rotationVelocity;

    private Transform orientation;

    private float yRotation;

    private void Awake()
    {
        _gameInput = FindObjectOfType<GameInput>();
    }

    public void SetGameInput(GameInput gameInput)
    {
        _gameInput = gameInput;
    }
    
    public void RemoveGameInput()
    {
        _gameInput = null;
    }
    private void LateUpdate()
    {
        if (_gameInput != null)
        {
            Rotating();
        }
    }

    private void Rotating()
    {
        _rotationVelocity = _gameInput.GetLookVector().x * rotateSpeed * Time.deltaTime ;
        player.transform.Rotate(Vector3.up * _rotationVelocity);
    }
}
