using System;
using DefaultNamespace;
using DefaultNamespace.Touchable;
using New.Player.Absctract;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private TMP_Text _levelText;

    public event Action<InventoryItemMono> OnItemTouched;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private void Update()
    {
        _levelText.text = LevelManager.levels.ToString();
    }

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
