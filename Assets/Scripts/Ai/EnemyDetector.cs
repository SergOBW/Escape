using System;
using System.Collections.Generic;
using New.Player.Detector;
using New.Player.Detector.Absctract;
using UnityEngine;

namespace DefaultNamespace.Ai
{
    public class EnemyDetector : Detector
    {
        public event Action<WalkPoint> OnWalkPointReached;
        public event Action<Player> OnPlayerReached;
        
        public event Action<WalkPoint> OnWalkPointReleased;
        public event Action<Player> OnPlayerReleased;

        private List<WalkPoint> _walkPoints;

        private void Start()
        {
            _walkPoints = new List<WalkPoint>();
        }

        public override void Detect(IDetectableObject detectableObject)
        {
            if (detectableObject.gameObject.TryGetComponent(out Player player))
            {
                // First Collider trigger
                OnPlayerReached?.Invoke(player);
            }
        }

        public override void ReleaseDetection(IDetectableObject detectableObject)
        {
            if (detectableObject.gameObject.TryGetComponent(out Player player))
            {
                // First Collider trigger
                OnPlayerReleased?.Invoke(player);
            }
        }

        private void HandleEnterWayPointStatus(WalkPoint walkPoint)
        {
            var wayPointStatus = walkPoint.wayPointStatus;
            switch (walkPoint.wayPointStatus)
            {
                case WayPointStatus.None :
                    // First Collider trigger
                    wayPointStatus = WayPointStatus.Near;
                    break;
                case WayPointStatus.Near :
                    // Second Collider trigger
                    wayPointStatus = WayPointStatus.Step;
                    OnWalkPointReached?.Invoke(walkPoint);
                    break;
            }

            walkPoint.wayPointStatus = wayPointStatus;
        }

        private void HandleReleaseWayPointSatus(WalkPoint walkPoint)
        {
            OnWalkPointReleased?.Invoke(walkPoint);
            walkPoint.wayPointStatus = WayPointStatus.None;

            if (_walkPoints.Contains(walkPoint))
            {
                _walkPoints.Remove(walkPoint);
            }
        }
    }
}