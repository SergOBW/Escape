using DefaultNamespace.Ai;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField]private WalkPoint[] _pointsToMove;
    private WalkPoint targetWayPoint;
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

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _detector = GetComponent<EnemyDetector>();
        _detector.OnPlayerReached += DetectorOnPlayerReached;
        walkPointIndex = 0;
        _navMeshAgent.speed = walkStartSpeed;
    }
    
    private void Start()
    {
        startPos = transform.position;
        targetWayPoint = _pointsToMove[walkPointIndex];
        targetPosition = targetWayPoint.my_position;
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
            MoveToTarget(targetPosition);
            if (_navMeshAgent.remainingDistance <= 0)
            {
                HandleTarget();
            }
        }
        else if (isFollowPlayer && targetPlayer != null)
        {
            MoveToTarget(targetPlayer.transform.position);
            if (_navMeshAgent.remainingDistance > attackEndRange)
            {
                Debug.Log("Attack player");
                targetPosition = lastWayPoint.transform.position;
                SetDefaults();
            }
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
