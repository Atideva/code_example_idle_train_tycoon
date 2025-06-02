using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.Railways;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.World.Maps
{
    public class MapGraph : SerializedMonoBehaviour
    {
        [SerializeField] [ReadOnly] private Dictionary<Waypoint, List<Waypoint>> _graph = new();
        public IReadOnlyDictionary<Waypoint, List<Waypoint>> Graph => _graph;

        public void Refresh(Railway[] rails)
        {
            _graph = Build(rails);
        }

        private Dictionary<Waypoint, List<Waypoint>> Build(Railway[] rails)
        {
            _graph = new Dictionary<Waypoint, List<Waypoint>>();

            foreach (var rail in rails)
            {
                if (!_graph.ContainsKey(rail.From)) _graph[rail.From] = new List<Waypoint>();
                if (!_graph.ContainsKey(rail.To)) _graph[rail.To] = new List<Waypoint>();

                _graph[rail.From].Add(rail.To);
                _graph[rail.To].Add(rail.From);
            }

            return _graph;
        }
    }
}