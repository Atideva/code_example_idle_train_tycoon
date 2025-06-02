using System;
using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Supplies;
using IdleTrainTycoon.Code.Gameplay.Trains.Data;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints.Data;
using ProjectScripts.Vibrant.vModules.Vibrant_Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Trains
{
    public class Train : MonoBehaviour
    {
        [SerializeField] private GameObject cargoContainer;
        [SerializeField] private SpriteRenderer cargoSprite;
        [SerializeField] private TrainMovement movement;
        [SerializeField] private TextMeshPro nameTxt;

        [Header(("Debug"))]
        [SerializeField] [Min(0)] private float harvestDurationSec = 10f;
        [SerializeField] [ReadOnly] private SupplySO cargo;

        public float HarvestDurationSec => harvestDurationSec;
        public Waypoint CurrentWaypoint => movement.CurrentWaypoint;
        public float Speed => movement.Speed;
        public SupplySO Cargo => cargo;

        public event Action<Train> OnReadyForNextJob = delegate { };
        public event Action<Train, Waypoint> OnFinishRoute = delegate { };
        public event Action<Train, SupplySO> OnLoaded = delegate { };

        public void Init(TrainSO so)
        {
            nameTxt.text = so.Name;

            movement.SetSpeed(so.Speed);
            movement.SetTransform(transform);
            movement.OnArrive += ArriveAt;
            harvestDurationSec = so.HarvestDurationSec;
        }

        public void ReadyToWork() => OnReadyForNextJob(this);
        private void ArriveAt(Waypoint waypoint) => OnFinishRoute(this, waypoint);
        
        public void PutInto(Waypoint waypoint) 
            => movement.SetCurrent(waypoint);
        public void MoveTo(Waypoint waypoint, List<WaypointData> route)
            => movement.MoveTo(waypoint, route);

        public void Loaded(SupplySO supply)
        {
            cargo = supply;
            cargoContainer.Enable();
            cargoSprite.sprite = supply.Icon;
            OnLoaded(this, supply);
        }

        public void Unload()
        {
            cargo = null;
            cargoContainer.Disable();
        }

    }
}