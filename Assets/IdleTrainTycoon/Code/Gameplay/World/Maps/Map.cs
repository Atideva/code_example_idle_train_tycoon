using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Railways;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.World.Maps
{
    [ExecuteInEditMode]
    public class Map : MonoBehaviour
    {
        [Space(20)]
        [SerializeField] private bool refreshMapInEditor;
        [Space(20)]
        [SerializeField] private MapGraph graph;
        [SerializeField] private Transform waypointsContainer;
        [SerializeField] private Transform railwaysContainer;
        [Space(20)]
        [SerializeField, ReadOnly] private Railway[] railways;
        [SerializeField, ReadOnly] private Waypoint[] waypoints;
        [SerializeField, ReadOnly] private List<Mine> mines = new();
        [SerializeField, ReadOnly] private List<Station> stations = new();

        public IReadOnlyList<Railway> Railways => railways;
        public IReadOnlyList<Mine> Mines => mines;
        public IReadOnlyList<Station> Stations => stations;
        public IReadOnlyList<Waypoint> Waypoints => waypoints;
        public IReadOnlyDictionary<Waypoint, List<Waypoint>> Graph => graph.Graph;
        public Waypoint GetRandomWaypoint() => waypoints[Random.Range(0, waypoints.Length)];
        public Station GetRandomStation() => stations[Random.Range(0, stations.Count)];


#if UNITY_EDITOR
        private void Update()
        {
            if (!refreshMapInEditor) return;
            if (Application.isPlaying) return;
            Init();
        }
#endif


 

        [Button]
        public void Init()
        {
            waypoints = FindWaypoints(waypointsContainer);
            railways = FindRailways(railwaysContainer);
            mines = FindMines(waypoints);
            stations = FindStations(waypoints);

            Connect(waypoints, railways);
            Refresh(railways);
            graph.Refresh(railways);
        }

        private static List<Station> FindStations(Waypoint[] ways)
        {
            List<Station> result = new();
            foreach (var waypoint in ways)
            {
                if (waypoint is Station m) result.Add(m);
            }

            return result;
        }

        private static List<Mine> FindMines(Waypoint[] ways)
        {
            List<Mine> result = new();
            foreach (var waypoint in ways)
            {
                if (waypoint is Mine m) result.Add(m);
            }

            return result;
        }

        private static Waypoint[] FindWaypoints(Transform container) => container.GetComponentsInChildren<Waypoint>();
        private static Railway[] FindRailways(Transform container) => container.GetComponentsInChildren<Railway>();

        private static void Refresh(Railway[] way)
        {
            foreach (var rail in way) rail.Refresh();
        }

        private static void Connect(Waypoint[] wayPoints, Railway[] rails)
        {
            foreach (var wayPoint in wayPoints)
                wayPoint.ClearConnections();

            foreach (var rail in rails)
            {
                if (!rail.From || !rail.To) continue;

                rail.From.Connect(rail.To, rail.Distance);
                rail.To.Connect(rail.From, rail.Distance);
            }
        }
    }
}