using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;

namespace IdleTrainTycoon.Code.Systems.RouteAlgorithms
{
    public class RouteRequest
    {
        public readonly Waypoint From;
        public readonly Waypoint To;
        public IReadOnlyList<Waypoint > Graph;

        public RouteRequest(Waypoint start, Waypoint goal, IReadOnlyList<Waypoint > graph)
        {
            From = start;
            To = goal;
            Graph = graph;
        }
    }
}