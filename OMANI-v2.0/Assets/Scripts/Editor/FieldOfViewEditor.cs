using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(LookDirectionsAndOrder))]
public class FieldOfViewEditor : Editor {
    private void OnSceneGUI()
    {
        LookDirectionsAndOrder fov = (LookDirectionsAndOrder)target;
        Handles.color = Color.white;
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, viewAngleA, fov.viewAngle, fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        Handles.color = Color.red;
        if (fov.closestTarget != null) { 
        Handles.DrawLine(fov.transform.position, fov.closestTarget.transform.position);
        }
    }
}
