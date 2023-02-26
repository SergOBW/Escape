using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameInput : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private void Awake()
        {
            _playerControls = new PlayerControls();

            _playerControls.Player.Enable();
        }

        public PlayerControls GetPlayerControls()
        {
            return _playerControls;
        }

        public Vector2 GetMovementVectorNormalizedMove()
        {
            Vector2 inputVector = _playerControls.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }
        
        public Vector2 GetMovementVectorNormalizedLook()
        {
            Vector2 inputVector = _playerControls.Player.Look.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }
        
        public Vector2 GetMovementVectorMove()
        {
            Vector2 inputVector = _playerControls.Player.Move.ReadValue<Vector2>();

            return inputVector;
        }
        
        public Vector2 GetMovementVectorLook()
        {
            Vector2 inputVector = _playerControls.Player.Look.ReadValue<Vector2>();

            return inputVector;
        }

        public bool IsMoving()
        {
            Vector2 inputVector = _playerControls.Player.Move.ReadValue<Vector2>();
            var value = Math.Abs(inputVector.x);
            if (value > 0)
            {
                return true;
            }

            return false;
        }
    }
}