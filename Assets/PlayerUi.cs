using System;
using DefaultNamespace;
using DefaultNamespace.Touchable;
using New.Player.Absctract;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;

    public event Action<InventoryItemMono> OnItemTouched;

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
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        float interactDistance = 5f;
        if (Physics.Raycast(ray,out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.collider.TryGetComponent(out InventoryItemMono inventoryItemMono))
            {
                if (inventoryItemMono.TryGetComponent(out ITouchable touchable))
                {
                    OnItemTouched?.Invoke(inventoryItemMono);
                    touchable.Interact();
                }
            }
            
            Debug.DrawLine(ray.origin,raycastHit.point,Color.red);
        }
    }
    
}
