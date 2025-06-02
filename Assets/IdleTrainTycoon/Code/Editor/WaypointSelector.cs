using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
#if UNITY_EDITOR
using UnityEditor;
#endif

[InitializeOnLoad]
public static class WaypointSelector
{
    static Waypoint _last;

    static WaypointSelector()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    static void OnSelectionChanged()
    {
        var selected = Selection.activeGameObject;
        var current = selected ? selected.GetComponent<Waypoint>() : null;

        if (_last && _last != current) _last.Unselected();
        if (current && _last != current) current.Selected();

        _last = current;
    }
}