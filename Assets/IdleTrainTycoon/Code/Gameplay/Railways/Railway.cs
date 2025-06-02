using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


namespace IdleTrainTycoon.Code.Gameplay.Railways
{
    public class Railway : MonoBehaviour
    {
        [SerializeField] private Waypoint from;
        [SerializeField] private Waypoint to;
        [SerializeField] private float distance;
        [Space(20)]
        [SerializeField]
        private bool showSettings;
        [ShowIf(nameof(showSettings))] [SerializeField] private float lineWidth = 1f;
        [ShowIf(nameof(showSettings))] [SerializeField] private Color lineColor;
        [ShowIf(nameof(showSettings))] [SerializeField] private LineRenderer line;
        [ShowIf(nameof(showSettings))] [SerializeField] private TextMeshPro distanceTxt;
        [ShowIf(nameof(showSettings))] [SerializeField] private Vector2 distanceOffset;

        private bool NotConnected => !from || !to;
        public Waypoint From => from;
        public Waypoint To => to;
        public float Distance => distance;

        private void OnValidate()
        {
            RefreshGameObjectName();
            RefreshNameTxt();
            RefreshPosition();
            RefreshLine();
        }

        public void Refresh()
        {
            RefreshNameTxt();
            RefreshPosition();
            RefreshLine();
        }

        private void RefreshGameObjectName()
        {
            if (NotConnected) return;
            name = "Railway | " + from.Name + " -> " + to.Name;
        }

        private void RefreshNameTxt()
        {
            if (!distanceTxt) return;
            distanceTxt.text = distance.ToString("F0") + " km";
            distanceTxt.transform.localPosition = distanceOffset;
        }

        private void RefreshPosition()
        {
            if (NotConnected) return;

            Vector3 midpoint = (from.Pos + to.Pos) / 2f;
            transform.position = midpoint;
        }

        private void ChangeLineColor(Color color)
        {
            line.startColor = color;
            line.endColor = color;
        }

        private void RefreshLine()
        {
            if (NotConnected) return;

            line.positionCount = 2;
            line.useWorldSpace = true;
            line.SetPosition(0, from.Pos);
            line.SetPosition(1, to.Pos);
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
        }

        public void OnSelected()
        {
            ChangeLineColor(Color.green);
            from.Selected();
            to.Selected();
            distanceTxt.color = Color.green;
        }

        public void OnUnselected()
        {
            ChangeLineColor(lineColor);
            from.Unselected();
            to.Unselected();
            distanceTxt.color = Color.black;
        }
    }
}