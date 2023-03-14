using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameInput : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private Joystick _dynamicJoystick;
        private void Awake()
        {
            _playerControls = new PlayerControls();

            _playerControls.Player.Enable();
        }

        public void SetJoystick(Joystick joystick)
        {
            _dynamicJoystick = joystick;
        }

        public PlayerControls GetPlayerControls()
        {
            return _playerControls;
        }

        public Vector2 GetMovementVectorNormilized()
        {
            var inputVector = GetMoveVector().normalized;
            
            return inputVector;
        }

        public Vector2 GetMoveVector()
        {
            Vector2 inputVector = new Vector2(_dynamicJoystick.Horizontal,_dynamicJoystick.Vertical);

            return inputVector;
        }
        
        public Vector2 GetLookVector()
        {
            Vector2 inputVector = new Vector2(_dynamicJoystick.Horizontal,_dynamicJoystick.Vertical);

            return inputVector;
        }

        public bool IsMoving()
        {
            Vector2 inputVector = new Vector2(_dynamicJoystick.Horizontal,_dynamicJoystick.Vertical);
            var value = Math.Abs(inputVector.x);
            if (value > 0)
            {
                return true;
            }

            return false;
        }
    }
}