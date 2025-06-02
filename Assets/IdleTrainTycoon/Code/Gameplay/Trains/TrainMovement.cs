using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace IdleTrainTycoon.Code.Gameplay.Trains
{
    public class TrainMovement : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float speed = 1f;
        [SerializeField] private Waypoint currentWaypoint;

        [Header("Debug")]
        [SerializeField] [ReadOnly] private List<WaypointData> _currentRoute;

        public Waypoint CurrentWaypoint => currentWaypoint;
        public event Action<Waypoint> OnArrive = delegate { };
        public float Speed => speed;
        private Transform moveTransform;

        public void SetTransform(Transform t)
        {
            moveTransform = t;
        }

        public void SetSpeed(float spd)
        {
            speed = spd;
        }

        public void MoveTo(Waypoint waypoint, List<WaypointData> route)
        {
            _currentRoute = route;
            StartMovement(waypoint, route).Forget();
        }

        private async UniTask StartMovement(Waypoint waypoint, List<WaypointData> route)
        {
            transform.position = currentWaypoint.Pos;
            await Movement(waypoint, route);
            OnArrive(waypoint);
        }

        private async UniTask Movement(Waypoint waypoint, List<WaypointData> route)
        {
            foreach (var target in route)
            {
                await MoveJob(target);
            }
        }

        public void SetCurrent(Waypoint waypoint)
        {
            Debug.Log($"{moveTransform.name} reach {waypoint.name}");
            currentWaypoint = waypoint;
            moveTransform.position = waypoint.Pos;
        }

        private async UniTask MoveJob(WaypointData target)
        {
            var duration = target.distance / Speed;
            var moveTween = transform.DOMove(target.waypoint.Pos, duration).SetEase(Ease.Linear);
            await moveTween.AsyncWaitForCompletion();
            SetCurrent(target.waypoint);
        }
    }
}