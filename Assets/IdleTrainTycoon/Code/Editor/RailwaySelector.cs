using IdleTrainTycoon.Code;
using IdleTrainTycoon.Code.Gameplay.Railways;
using UnityEditor;

[InitializeOnLoad]
public static class RailwaySelector
{
    static Railway _last;

    static RailwaySelector()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    static void OnSelectionChanged()
    {
        var selected = Selection.activeGameObject;
        var current = selected ? selected.GetComponent<Railway>() : null;

        if (_last && _last != current) _last.OnUnselected();
        if (current && _last != current) current.OnSelected();

        _last = current;
    }

}