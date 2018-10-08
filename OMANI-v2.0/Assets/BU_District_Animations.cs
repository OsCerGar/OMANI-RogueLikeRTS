using System.Collections.Generic;
using UnityEngine;

public class BU_District_Animations : MonoBehaviour
{


    public List<Light> LampLights = new List<Light>();

    // Use this for initialization
    void Start()
    {
        foreach (Transform lamp in transform.Find("Lamps"))
        {
            LampLights.Add(lamp.GetComponentInChildren<Light>());
        }

        energyLevel(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void energyLevel(int level)
    {
        switch (level)
        {
            case 0:
                LampLevels(level);
                break;
            case 1:
                LampLevels(level);

                break;
            case 2:
                LampLevels(level);
                break;
            case 3:
                LampLevels(level);
                break;
        }

    }

    private void LampLevels(int level)
    {
        switch (level)
        {
            case 0:
                foreach (Light light in LampLights)
                {
                    light.intensity = 0;
                }
                break;
            case 1:
                foreach (Light light in LampLights)
                {
                    light.intensity = 5;
                }
                break;
            case 2:
                foreach (Light light in LampLights)
                {
                    light.intensity = 7.5f;
                }
                break;
            case 3:
                foreach (Light light in LampLights)
                {
                    light.intensity = 10;
                }
                break;
        }
    }
}
