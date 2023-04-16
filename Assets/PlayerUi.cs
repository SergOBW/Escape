using System;
using System.Collections.Generic;
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
    private Dictionary<GameObject, bool> touchedDictionary;

    public event Action<InventoryItemMono> OnItemTouched;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private void Awake()
    {
        _gameInput = FindObjectOfType<GameInput>();
        SetGameInput(_gameInput);
    }

    public void SetGameInput(GameInput gameInput)
    {
        _gameInput = gameInput;
        touchPositionAction = _gameInput.GetPlayerControls().FindAction("TouchPosition");
        touchPressAction = _gameInput.GetPlayerControls().FindAction("TouchPress");
        touchPressAction.performed += TouchOnpPerformed;
        touchedDictionary = new Dictionary<GameObject, bool>();
    }
    
    private void Update()
    {
        _levelText.text = LevelManager.levels.ToString();
    }
    
    private void TouchOnpPerformed(InputAction.CallbackContext obj)
    {
        var touchPosition = touchPositionAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        float interactDistance = 2f;
        if (Physics.Raycast(ray,out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.collider.TryGetComponent(out ITouchable touchable) && !touchedDictionary.ContainsKey(raycastHit.collider.gameObject))
            {
                if (raycastHit.collider.TryGetComponent(out InventoryItemMono inventoryItemMono))
                {
                    OnItemTouched?.Invoke(inventoryItemMono);
                }
                touchable.Interact();
                // Double touch secure for no duping
                touchedDictionary.Add(raycastHit.collider.gameObject,true);
            }

            if (touchedDictionary.ContainsKey(raycastHit.collider.gameObject))
            {
                Debug.LogWarning("Double touch secure for no duping");
            }
        }
    }
    
}
