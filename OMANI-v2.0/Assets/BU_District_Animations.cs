using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_District_Animations : MonoBehaviour
{


    public List<Light> LampLights = new List<Light>();
    private List<MeshRenderer> GroundLights = new List<MeshRenderer>();
    [SerializeField]
    private BU_Energy_CityDistricts cityDistrict;
    private Text points;

    // Use this for initialization
    void Start()
    {
        /*
        foreach (Transform lamp in transform.Find("Lamps"))
        {
            LampLights.Add(lamp.GetComponentInChildren<Light>());
        }
        */

        foreach (Transform groundLight in transform.Find("GroundLights"))
        {
            GroundLights.Add(groundLight.GetComponent<MeshRenderer>());
        }
        points = transform.Find("Canvas").GetComponentInChildren<Text>();

        cityDistrict = transform.GetComponentInParent<BU_Energy_CityDistricts>();
        energyLevel(0);
    }

    public void repeaterAnimation()
    {
        /*
        foreach (Interactible_Repeater repeater in cityDistrict.energyDistrict.returnRepeatersEnergy())
        {
            if (repeater.energy > 0)
            {
                repeater.EnnuAnimation();
            }
        }
    
    */}

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
                foreach (MeshRenderer groundLight in GroundLights)
                {
                    groundLight.material.color = Color.white;
                }
                for (int i = 0; i < 3; i++)
                {
                    GroundLights[i].material.color = Color.white;
                }
                break;
            case 1:
                foreach (MeshRenderer groundLight in GroundLights)
                {
                    groundLight.material.color = Color.white;
                }
                for (int i = 0; i < 1; i++)
                {
                    GroundLights[i].material.color = Color.yellow;
                }

                break;
            case 2:
                foreach (MeshRenderer groundLight in GroundLights)
                {
                    groundLight.material.color = Color.white;
                }
                for (int i = 0; i < 2; i++)
                {
                    GroundLights[i].material.color = Color.yellow;
                }
                break;
            case 3:
                foreach (MeshRenderer groundLight in GroundLights)
                {
                    groundLight.material.color = Color.white;
                }
                for (int i = 0; i < 3; i++)
                {
                    GroundLights[i].material.color = Color.yellow;
                }
                break;
        }

    }

    public void totalEnnus(int _totalEnnus)
    {
        points.text = _totalEnnus.ToString();
    }
}
