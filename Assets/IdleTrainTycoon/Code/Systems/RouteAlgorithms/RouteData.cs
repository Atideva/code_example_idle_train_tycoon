using System.Collections.Generic;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints.Data;

namespace IdleTrainTycoon.Code.Systems.RouteAlgorithms
{
    [System.Serializable]
    public class RouteData
    {
        public float distance;
        public List<WaypointData> route = new();
    }
}