using DefaultNamespace;
using DefaultNamespace.PlayerStates;
using New.Player.Detector.Absctract;
using UnityEngine;

public class Player : MonoBehaviour, IDetectableObject
{
    private Vector3 lastInteractDir;
    private bool _isWalking;
    private bool siMoveFormed;
    [SerializeField]private CharacterController characterController;
    
    [SerializeField]
    private float moveSpeed = 7f;

    private GameInput _gameInput;

    #region DetectableObject
    public event ObjectDetectedHandler OnGameObjectDetectedEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;
    public void Detected(GameObject detectionSourse)
    {
        OnGameObjectDetectedEvent?.Invoke(detectionSourse, gameObject);
    }

    public void DetectionReleased(GameObject detectionSourse)
    {
        OnGameObjectDetectionReleasedEvent?.Invoke(detectionSourse,gameObject);
    }

    #endregion

    public void SetGameInput(GameInput gameInput)
    {
        _gameInput = gameInput;
    }
    private void FixedUpdate()
    {
        if (GameStateManager.Instance.currentGameState.GetType() == typeof(GamePlayingState) && _gameInput != null)
        {
            HandleMovement();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LevelManager.Win();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            LevelManager.SetDefaults();
        }
    }


    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
        Vector3 moveDir = transform.forward.normalized * inputVector.y;

        float moveDistance = moveSpeed * Time.fixedDeltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        //bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius,moveDir, moveDistance);
        
        if (_gameInput.IsMoving())
        {
            //transform.position += moveDir * moveDistance;
            characterController.Move(moveDir * moveDistance);
        }
        
        //transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        _isWalking = moveDir != Vector3.zero;
    }

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
        if (inputVector!= null)
        {
            Vector3 moveDir = transform.forward.normalized * inputVector.y;
            //Gizmos.DrawRay(transform.forward,moveDir);
        }
    }
    #endregion
    
}
