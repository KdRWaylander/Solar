using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapGenerator mG = (MapGenerator)target;

        if (GUILayout.Button("REGENERATE"))
        {
            if (EditorApplication.isPlaying)
                mG.RegenerateTerrain();
        }
    }
}