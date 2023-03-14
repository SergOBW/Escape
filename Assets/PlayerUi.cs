using System;
using DefaultNamespace;
using DefaultNamespace.Touchable;
using New.Player.Absctract;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUi : MonoBehaviour
{
    private GameInput _gameInput;
    [SerializeField] private TMP_Text _levelText;

    public event Action<InventoryItemMono> OnItemTouched;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    public void SetGameInput(GameInput gameInput)
    {
        _gameInput = gameInput;
        touchPositionAction = _gameInput.GetPlayerControls().FindAction("TouchPosition");
        touchPressAction = _gameInput.GetPlayerControls().FindAction("TouchPress");
        touchPressAction.performed += TouchOnpPerformed;
    }

    private void Update()
    {
        _levelText.text = LevelManager.levels.ToString();
    }
    
    private void OnDisable()
    {
        touchPressAction.performed -= TouchOnpPerformed;
    }

    private void TouchOnpPerformed(InputAction.CallbackContext obj)
    {
        var touchPosition = touchPositionAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        float interactDistance = 2f;
        if (Physics.Raycast(ray,out RaycastHit raycastHit, interactDistance))
        {
            
            if (raycastHit.collider.TryGetComponent(out ITouchable touchable))
            {
                if (raycastHit.collider.TryGetComponent(out InventoryItemMono inventoryItemMono))
                {
                    OnItemTouched?.Invoke(inventoryItemMono);
                }
                touchable.Interact();
            }

            Debug.DrawLine(ray.origin,raycastHit.point,Color.red);
        }
    }
    
}
