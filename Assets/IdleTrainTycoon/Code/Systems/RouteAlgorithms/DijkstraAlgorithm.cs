using System.Collections.Generic;
using System.Linq;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints.Data;

namespace IdleTrainTycoon.Code.Systems.RouteAlgorithms
{
    public class DijkstraAlgorithm : IRouteAlgorithm
    {
        public List<RouteData> GetPossible(RouteRequest request)
        {
            var possibile = new List<RouteData>();
            var current = new List<WaypointData>();
            var visited = new HashSet<Waypoint>();
            var goal = request.To;

            void DFS(WaypointData wp)
            {
                visited.Add(wp.waypoint);
                current.Add(wp);

                if (wp.waypoint == goal)
                    newRoute(current);
                else
                    checkNeighbors(wp.waypoint);

                current.RemoveAt(current.Count - 1);
                visited.Remove(wp.waypoint);
            }

            DFS(new WaypointData(request.From, 0));

            return possibile;

            void newRoute(List<WaypointData> list)
            {
                var route = list.ToList();
                var dist = list.Sum(d => d.distance);
                possibile.Add(new RouteData { distance = dist, route = route });
            }

            void checkNeighbors(Waypoint waypoint)
            {
                foreach (var neighbor in waypoint.Neighbors)
                {
                    if (visited.Contains(neighbor.waypoint)) continue;
                    DFS(neighbor);
                }
            }
        }

        public RouteData GetShortest(RouteRequest data)
        {
            var possible = GetPossible(data)
                .OrderBy(d => d.distance)
                .ToList();
            return possible[0];
        }
    }
}