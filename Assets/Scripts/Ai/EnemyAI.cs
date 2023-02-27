using DefaultNamespace.Ai;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField]private WalkPoint[] _pointsToMove;
    private Vector3 targetPos;
    private Vector3 startPos;
    private EnemyDetector _detector;
    private int walkPointIndex;
    private bool isWalkingRecursive;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _detector = GetComponent<EnemyDetector>();
        _detector.OnWalkPointReached += OnWalkPointReached;
        walkPointIndex = 0;
    }

    private void Start()
    {
        startPos = transform.position;
        targetPos = _pointsToMove[walkPointIndex].my_position;
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

    private void Idle()
    {
        targetPos = startPos;
    }
    private void MoveToTarget(Vector3 targetPos)
    {
        _navMeshAgent.destination = targetPos;
    }

    private void Update()
    {
        MoveToTarget(targetPos);
    }
}
