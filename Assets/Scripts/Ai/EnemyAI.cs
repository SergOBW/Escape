using DefaultNamespace.Ai;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField]private WalkPoint[] _pointsToMove;
    [SerializeField]private WalkPoint targetWayPoint;
    private Player targetPlayer;
    [SerializeField]private float attackEndRange = 5;
    private Vector3 targetPosition;
    private Vector3 startPos;
    private WalkPoint lastWayPoint;
    private EnemyDetector _detector;
    private int walkPointIndex;
    private bool isWalkingRecursive;
    private bool isFollowPlayer;
    [SerializeField] private float walkStartSpeed = 3;
    private bool moveToNextTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _detector = GetComponent<EnemyDetector>();
        _detector.OnPlayerReached += DetectorOnPlayerReached;
    }
    
    private void Start()
    {
        walkPointIndex = 0;
        _navMeshAgent.speed = walkStartSpeed;
        startPos = transform.position;
        // first wayPoint
        targetWayPoint = _pointsToMove[walkPointIndex];
        targetPosition = targetWayPoint.my_position;
        //Debug.Log("Next targtet index = " + walkPointIndex + " Name " + targetWayPoint.transform.name + " Position " + targetPosition);
    }
    
    private void Update()
    {
        HandleMovement();
    }

    private void SetDefaults()
    {
        isFollowPlayer = false;
        _navMeshAgent.speed = walkStartSpeed;
        targetPlayer = null;
    }
    
    private void DetectorOnPlayerReached(Player player)
    {
        Debug.LogError(player.name + " was found");
        float speedWhenPlayerReached = 1f;
        _navMeshAgent.speed = speedWhenPlayerReached;
        lastWayPoint = targetWayPoint;
        targetPlayer = player;
        targetPosition = targetPlayer.gameObject.transform.position;
        isFollowPlayer = true;
    }
    

    private void FindNextTarget()
    {
        var nextIndex = walkPointIndex + 1;
        var arrayCount = _pointsToMove.Length - 1;
        if (nextIndex < arrayCount)
        {
            walkPointIndex++;
            targetWayPoint = _pointsToMove[walkPointIndex];
            targetPosition = targetWayPoint.my_position;
        } else if (nextIndex == arrayCount)
        {
            walkPointIndex = nextIndex;
            targetWayPoint = _pointsToMove[walkPointIndex];
            targetPosition = targetWayPoint.my_position;
            isWalkingRecursive = true;
        }
        
        //Debug.Log("Next target index = " + walkPointIndex + " Name " + targetWayPoint.transform.name + " Position " + targetPosition);
    }
    private void HandleTarget()
    {
        if (_pointsToMove.Length < 1)
        {
            Idle();
            return;
        }
        switch (isWalkingRecursive)
        {
            case true:
                FindNextTargetRecursive();
                break;
            case false:
                FindNextTarget();
                break;
        }
    }
    private void HandleMovement()
    {
        if (!isFollowPlayer)
        {
            if (_navMeshAgent.remainingDistance <= 0)
            {
                HandleTarget();
            }
            MoveToTarget(targetPosition);
        }
        else if (isFollowPlayer && targetPlayer != null)
        {
            MoveToTarget(targetPlayer.transform.position);
            if (_navMeshAgent.remainingDistance > attackEndRange || !_navMeshAgent.hasPath)
            {
                Debug.Log("No player");
                targetPosition = lastWayPoint.transform.position;
                SetDefaults();
            }
            //Debug.Log(_navMeshAgent.remainingDistance);
        }
    }
    
    private void FindNextTargetRecursive()
    {
        var nextIndex = walkPointIndex - 1;
        var arrayCount = 0;
        if (nextIndex > arrayCount)
        {
            walkPointIndex--;
            targetWayPoint = _pointsToMove[walkPointIndex];
            targetPosition = targetWayPoint.my_position;
        } else if (nextIndex == arrayCount)
        {
            walkPointIndex = nextIndex;
            targetWayPoint = _pointsToMove[walkPointIndex];
            targetPosition = targetWayPoint.my_position;
            isWalkingRecursive = false;
        }
        //Debug.Log("Next Recursive target index = " + walkPointIndex + " Name " + targetWayPoint.transform.name + " Position " + targetPosition);
    }

    private void Idle()
    {
        targetPosition = startPos;
    }
    private void MoveToTarget(Vector3 targetPos)
    {
        _navMeshAgent.destination = targetPos;
    }

    
}
