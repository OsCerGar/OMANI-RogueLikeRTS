using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_PowerPlant : MonoBehaviour
{
    [SerializeField]
    GameObject cablePrefab;
    [SerializeField]
    List<temporalCable> cables = new List<temporalCable>();
    public int energy;

    // Use this for initialization
    void Start()
    {
        foreach (temporalCable cable in this.transform.GetChild(0).GetChild(0).GetComponentsInChildren<temporalCable>())
        {
            cables.Add(cable);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addEnergy()
    {
        bool givenEnergy = false;
        this.energy += 1;
        int i = 0;
        while (givenEnergy == false && i < cables.Count)
        {
            if (cables[i].energy == false)
            {
                cables[i].energy = true;
                givenEnergy = true;
            }

            i++;
        }
    }

    public void removeEnergy()
    {
        bool removedEnergy = false;
        this.energy -= 1;
        int i = cables.Count - 1;
        while (removedEnergy == false && i >= 0)
        {
            if (cables[i].energy == true)
            {
                cables[i].energy = false;
                removedEnergy = true;
            }
            i--;
        }
    }
}
