using DefaultNamespace.Ai;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField]private WalkPoint[] _pointsToMove;
    private Vector3 targetPos;
    private Vector3 startPos;
    private Vector3 lastWayPoint;
    private EnemyDetector _detector;
    private int walkPointIndex;
    private bool isWalkingRecursive;
    private bool isFollowPlayer;
    [SerializeField] private float walkStartSpeed = 3;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _detector = GetComponent<EnemyDetector>();
        _detector.OnWalkPointReached += OnWalkPointReached;
        _detector.OnPlayerReached += DetectorOnPlayerReached;
        walkPointIndex = 0;
        _navMeshAgent.speed = walkStartSpeed;
    }
    private void Start()
    {
        startPos = transform.position;
        targetPos = _pointsToMove[walkPointIndex].my_position;
    }
    
    private void Update()
    {
        HandleMovement();
    }

    private void SetDefaults()
    {
        isFollowPlayer = false;
        _navMeshAgent.speed = walkStartSpeed;
    }
    
    private void DetectorOnPlayerReached(Player obj)
    {
        float speedWhenPlayerReached = 1f;
        _navMeshAgent.speed = speedWhenPlayerReached;
        lastWayPoint = targetPos;
        targetPos = obj.transform.position;
        isFollowPlayer = true;
    }

    private void FindNextTarget()
    {
        var nextIndex = walkPointIndex + 1;
        var arrayCount = _pointsToMove.Length - 1;
        if (nextIndex < arrayCount)
        {
            walkPointIndex++;
            targetPos = _pointsToMove[walkPointIndex].my_position;
        } else if (nextIndex == arrayCount)
        {
            walkPointIndex = nextIndex;
            targetPos = _pointsToMove[walkPointIndex].my_position;
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
            MoveToTarget(targetPos);
        }
        else if (isFollowPlayer)
        {
            Debug.Log(_navMeshAgent.remainingDistance);
            if (_navMeshAgent.remainingDistance < 1f)
            {
                targetPos = lastWayPoint;
                SetDefaults();
                Debug.Log("KillPlayer");
            }
            MoveToTarget(targetPos);
        }
    }
    
    
    private void FindNextTargetRecursive()
    {
        var nextIndex = walkPointIndex - 1;
        var arrayCount = 0;
        if (nextIndex > arrayCount)
        {
            walkPointIndex--;
            targetPos = _pointsToMove[walkPointIndex].my_position;
        } else if (nextIndex == arrayCount)
        {
            walkPointIndex = nextIndex;
            targetPos = _pointsToMove[walkPointIndex].my_position;
            isWalkingRecursive = false;
        }
    }

    private void OnWalkPointReached(WalkPoint walkPoint)
    {
        HandleTarget();
    }

    private void Idle()
    {
        targetPos = startPos;
    }
    private void MoveToTarget(Vector3 targetPos)
    {
        _navMeshAgent.destination = targetPos;
    }
    
}
