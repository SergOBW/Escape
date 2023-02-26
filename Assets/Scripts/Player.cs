using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;

    private Vector3 lastInteractDir;
    private bool _isWalking;
    private bool siMoveFormed;

    [SerializeField]
    private float moveSpeed = 7f;
    private void Update()
    {
        HandleMovement();
    }
    

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalizedMove();
        Vector3 moveDir = transform.forward.normalized * inputVector.y;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius,moveDir, moveDistance);
        
        if (canMove && _gameInput.IsMoving())
        {
            transform.position += moveDir * moveDistance;
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
        Vector2 inputVector = _gameInput.GetMovementVectorMove();
        if (inputVector!= null)
        {
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            Gizmos.DrawRay(transform.position,moveDir);
        }
    }
    #endregion
    
}