using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] private OnScreenStick _onScreenStick;
    [SerializeField] private GameInput _gameInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private void Awake()
    {
        touchPositionAction = _gameInput.GetPlayerControls().FindAction("TouchPosition");
        touchPressAction = _gameInput.GetPlayerControls().FindAction("TouchPress");
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchOnpPerformed;
    }
    
    private void OnDisable()
    {
        touchPressAction.performed -= TouchOnpPerformed;
    }

    private void TouchOnpPerformed(InputAction.CallbackContext obj)
    {
        var touchPosition = touchPositionAction.ReadValue<Vector2>();
        ResetJoystickPostition(touchPosition);
    }

    private void ResetJoystickPostition(Vector2 touchPosition)
    {
        var newPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        newPosition.z = 0;
        
        Debug.Log(newPosition);
        _onScreenStick.transform.position = newPosition;
    }
}
