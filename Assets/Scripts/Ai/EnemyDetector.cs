using System;
using New.Player.Detector;
using New.Player.Detector.Absctract;
using UnityEngine;

namespace DefaultNamespace.Ai
{
    public class EnemyDetector : Detector
    {
        public event Action<WalkPoint> OnWalkPointReached;
        public event Action<Player> OnPlayerReached;
        public override void Detect(IDetectableObject detectableObject)
        {
            Debug.Log(detectableObject);
            if (detectableObject.gameObject.TryGetComponent(out WalkPoint walkPoint))
            {
                OnWalkPointReached?.Invoke(walkPoint);
            }
            
            if (detectableObject.gameObject.TryGetComponent(out Player player))
            {
                OnPlayerReached?.Invoke(player);
            }
        }
    }
}