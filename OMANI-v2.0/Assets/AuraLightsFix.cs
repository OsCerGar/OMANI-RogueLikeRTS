using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AuraAPI;

[ExecuteInEditMode]
public class AuraLightsFix : MonoBehaviour
{

    [SerializeField]
    List<AuraLight> AuraLights;

    [SerializeField]
    List<AuraVolume> AuraLightVolume;

    private void Update()
    {
        if (!Application.isPlaying)
        {
            foreach (AuraLight light in AuraLights)
            {
                light.enabled = true;
            }
            foreach (AuraVolume volume in AuraLightVolume)
            {
                volume.enabled = true;
            }
        }
    }
}
