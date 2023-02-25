using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    
    private Vector3 lastInteractDir;
    private bool _isWalking;

    [SerializeField]
    private float moveSpeed = 7f;
    
    [SerializeField]
    private float rotateSpeed = 5f;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius,moveDir, moveDistance);
        
        if (!canMove)
        {
            //cannot move towards moveDir
           
            //attempt only X movement

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius,moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move x
               
                Vector3 moveDirZ = new Vector3(moveDir.z, 0, 0).normalized;
                canMove = moveDir.z != 0 &&!Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius,moveDirZ, moveDistance);

                //attempt only z

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move any
                }
            }
        }
        
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        
        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        
        Debug.Log("X = "+ moveDir.x + " Z =" + moveDir.z);
        
        _isWalking = moveDir != Vector3.zero;
    }
    #region Gizmos

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        if (inputVector!= null)
        {
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            Gizmos.DrawRay(transform.position,moveDir);
        }
    }
    #endregion
}
