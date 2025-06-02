using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Supplies;
using IdleTrainTycoon.Code.Gameplay.Trains;
using IdleTrainTycoon.Code.Gameplay.World.Maps;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Systems.BankSystem;
using IdleTrainTycoon.Code.Systems.RouteAlgorithms;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Towers
{
    public class DispatcherTower : SerializedMonoBehaviour, IRouteAlgorithm
    {
        [SerializeField] private bool ordersToFindMine = true;
        [SerializeField] private bool ordersToFindStation = true;

        [Space(20)]
        [FoldoutGroup("Test")]
        [SerializeField] private Waypoint fromWaypoint;
        [FoldoutGroup("Test")]
        [SerializeField] private Waypoint toWaypoint;
        [FoldoutGroup("Test")]
        [HideReferenceObjectPicker] public List<RouteData> possibleRoutes = new();

        private MineTower _tower;
        private Bank _bank;
        private Map _map;
        private IRouteAlgorithm _algorithm;

        public List<RouteData> GetPossible(RouteRequest data) => _algorithm.GetPossible(data);
        public RouteData GetShortest(RouteRequest data) => _algorithm.GetShortest(data);

        public void Init(Map newMap, MineTower tower, Bank bank)
        {
            _bank = bank;
            _tower = tower;
            _map = newMap;
            _algorithm = new DijkstraAlgorithm();
        }

        public void Observe(Train train)
        {
            train.OnReadyForNextJob += OrderToFindMine;
            train.OnFinishRoute += Arrived;
            train.OnLoaded += OrderToFindStation;
        }

        private void OrderToFindStation(Train train, SupplySO supply)
        {
            if (!ordersToFindStation) return;

            var mine = FindTopRevenueStation(train);
            var request = new RouteRequest(train.CurrentWaypoint, mine, _map.Waypoints);
            var way = GetShortest(request);
            train.MoveTo(mine, way.route);
        }

        private void Arrived(Train train, Waypoint destination)
        {
            Debug.Log((train.name + " has finish route"));

            switch (destination)
            {
                case Mine mine:
                    mine.StartLoadTrain(train);
                    break;
                case Station station:
                {
                    var sellPrice = station.GetOfferedPrice(train.Cargo);
                    _bank.AddGold(sellPrice);

                    station.Delivery(train.Cargo);
                    train.Unload();
                    train.ReadyToWork();
                    break;
                }
            }
        }

        private void OrderToFindMine(Train train)
        {
            if (!ordersToFindMine) return;
            Debug.Log((train.name + " ready for order"));

            var mine = FindBestMine(train);
            var request = new RouteRequest(train.CurrentWaypoint, mine, _map.Waypoints);
            var way = GetShortest(request);
            train.MoveTo(mine, way.route);

            var travelTime = GetTravelTime(train.CurrentWaypoint, mine, train.Speed);
            var harvestTime = train.HarvestDurationSec * mine.HarvestDurationMult;
            _tower.WillBeBusyAt(mine, train, travelTime, harvestTime);
        }

        private Station FindClosestStation(Train train)
        {
            Station goal = null;
            var minTime = float.MaxValue;
            foreach (var station in _map.Stations)
            {
                var travelTime = GetTravelTime(train.CurrentWaypoint, station, train.Speed);

                if (travelTime > minTime) continue;
                minTime = travelTime;
                goal = station;
            }

            return goal;
        }

        private Station FindTopRevenueStation(Train train)
        {
            Station goal = null;
            var bestRevenue = 0f;
            foreach (var station in _map.Stations)
            {
                var travelTime = GetTravelTime(train.CurrentWaypoint, station, train.Speed);
                var price = station.GetOfferedPrice(train.Cargo);
                var revenue = price / travelTime;
                if (revenue <= bestRevenue) continue;
                bestRevenue = revenue;
                goal = station;
            }

            return goal;
        }

        private Mine FindBestMine(Train train)
        {
            Mine goal = null;
            var minTime = float.MaxValue;
            var msg = "";
            foreach (var mine in _map.Mines)
            {
                var travelTime = GetTravelTime(train.CurrentWaypoint, mine, train.Speed);
                var availableAt = _tower.GetIdleDuration(mine);
                var idleTime = availableAt > travelTime ? availableAt - travelTime : 0;
                var harvestTime = train.HarvestDurationSec * mine.HarvestDurationMult;

                var totalTime = travelTime + idleTime + harvestTime;
                msg += $"\n({mine.name} | Total: {totalTime} = {travelTime} (travel) + {idleTime} (idle) + {harvestTime} (harvest). FreeAt: {availableAt} )";
                if (totalTime > minTime) continue;

                minTime = totalTime;
                goal = mine;
            }

            Debug.Log(train.name + " algorithm: " + msg + ".\n Goal mine: " + goal.name);
            return goal;
        }

        private float GetTravelTime(Waypoint from, Waypoint to, float speed)
        {
            var request = new RouteRequest(from, to, _map.Waypoints);
            var path = GetShortest(request);
            var dist = path.distance;
            return dist / speed;
        }


        [FoldoutGroup("Test")]
        [Button]
        private void FindRoutes()
        {
            var request = new RouteRequest(fromWaypoint, toWaypoint, _map.Waypoints);
            possibleRoutes = _algorithm.GetPossible(request);
            Debug.Log($"!!!Found {possibleRoutes.Count} paths from {fromWaypoint.name} to {toWaypoint.name}");
        }


    }
}