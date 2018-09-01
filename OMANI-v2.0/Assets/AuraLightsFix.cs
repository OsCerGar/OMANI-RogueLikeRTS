using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AuraAPI;

[ExecuteInEditMode]
public class AuraLightsFix : MonoBehaviour
{

    [SerializeField]
    List<AuraLight> AuraLights;

    private void Update()
    {
        if (!Application.isPlaying)
        {
            foreach (AuraLight light in AuraLights)
            {
                light.enabled = true;
            }
            Debug.Log("AuraLightFix in Update");
        }
    }
}
