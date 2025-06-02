using Sirenix.OdinInspector;

namespace IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints.Data
{
    [System.Serializable]
    public class WaypointData
    {
        [HorizontalGroup(Width = 100)] public float distance;
        [HorizontalGroup] [HideLabel] public Waypoint waypoint;

        public WaypointData(Waypoint waypoint, float distance)
        {
            this.distance = distance;
            this.waypoint = waypoint;
        }
    }
}