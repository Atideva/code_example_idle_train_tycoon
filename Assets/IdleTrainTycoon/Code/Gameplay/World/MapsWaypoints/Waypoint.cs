using System.Collections.Generic;
using System.Linq;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints
{
    public abstract class Waypoint : MonoBehaviour
    {

        [PropertyOrder(100)] [Space(20)]
        [SerializeField] private string nameLabel;
        [PropertyOrder(101)]
        [SerializeField] private TextMeshPro nameTxt;
        [PropertyOrder(102)] [Space(20)]
        [SerializeField] [ReadOnly] private List<WaypointData> neighbors = new();


        public Vector2 Pos => transform.position;
        public string Name => nameLabel;
        public IReadOnlyList<WaypointData> Neighbors => neighbors;


        public void Connect(Waypoint waypoint, float distance)
        {
            if (neighbors.Any(w => w.waypoint == waypoint)) return;
            var neighbor = new WaypointData(waypoint, distance);
            neighbors.Add(neighbor);
        }

        public void ClearConnections()
        {
            neighbors.Clear();
        }


        public void Selected()
        {
            transform.localScale = Vector3.one * 1.3f;
            nameTxt.color = Color.green;
        }

        public void Unselected()
        {
            transform.localScale = Vector3.one * 1f;
            nameTxt.color = Color.black;
        }
    }
}