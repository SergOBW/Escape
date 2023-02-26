using System;
using System.Collections;
using DefaultNamespace.Ai;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField]private WalkPoint[] _pointsToMove;
    private Vector3 targetPos;
    private EnemyDetector _detector;
    private int walkPointIndex = 0;
    private bool isReachTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _detector = GetComponentInChildren<EnemyDetector>();
        _detector.OnWalkPointReached += OnWalkPointReached;
    }

    private void Start()
    {
        targetPos = _pointsToMove[walkPointIndex].my_position;
        StartCoroutine(FindNextTarget());
    }

    private IEnumerator FindNextTarget()
    {
        yield return new WaitUntil(FindNextPos);
        walkPointIndex++;
        if (walkPointIndex <= _pointsToMove.Length -1)
        {
            targetPos = _pointsToMove[walkPointIndex].my_position;
            StartCoroutine(FindNextTarget());
        }
    }

    private void OnWalkPointReached(WalkPoint walkPoint)
    {
        walkPoint.DestroySelf();
    }

    private bool FindNextPos()
    {
        return isReachTarget;
    }
    
    private void MoveToTarget(Vector3 targetPos)
    {
        _navMeshAgent.destination = targetPos;
    }

    private void Update()
    {
       MoveToTarget(targetPos);
       if (_navMeshAgent.remainingDistance <= 0f)
       {
           isReachTarget = true;
       }
       else
       {
           isReachTarget = false;
       }
    }
}
