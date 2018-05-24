
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Map myScript = (Map)target;

        if (GUILayout.Button("Build Object"))
        {
            myScript.GenerateMap();
        }
    }
}
