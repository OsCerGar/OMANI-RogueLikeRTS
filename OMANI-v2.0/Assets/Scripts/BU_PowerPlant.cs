using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_PowerPlant : MonoBehaviour
{
    [SerializeField]
    GameObject cablePrefab;
    List<temporalCable> cables = new List<temporalCable>();

    // Use this for initialization
    void Start()
    {

        foreach (temporalCable cable in this.transform.GetChild(0).GetComponentsInChildren<temporalCable>())
        {
            cables.Add(cable);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))
        {
            addEnergy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("People"))
        {
            removeEnergy();
        }

    }

    void addEnergy()
    {
        bool givenEnergy = false;
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
    void removeEnergy()
    {
        bool removedEnergy = false;
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
