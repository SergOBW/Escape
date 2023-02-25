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

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = _playerControls.Player.MoveAndLook.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }
    }
}