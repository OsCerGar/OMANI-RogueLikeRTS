using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Power_Office : MonoBehaviour
{

    public BoxCollider myBoxCollider;
    public MeshRenderer[] buttons;
    private Transform[] positions;
    int thisEnergy;
    BU_PowerPlant powerPlant;

    private void Awake()
    {
        buttons = this.transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
        positions = this.transform.GetChild(1).GetComponentsInChildren<Transform>();
        powerPlant = this.transform.parent.GetComponent<BU_PowerPlant>();

        //Adds positions to the powerplant
        foreach (Transform position in positions)
        {
            powerPlant.positions.Add(position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People") && thisEnergy < 2)
        {
            addEnergy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("People") && thisEnergy > 0)
        {
            removeEnergy();
        }

    }
    void addEnergy()
    {
        powerPlant.addEnergy();
        buttons[thisEnergy].material.color = Color.yellow;
        thisEnergy = thisEnergy + 1;

    }

    void removeEnergy()
    {
        thisEnergy = thisEnergy - 1;
        buttons[thisEnergy].material.color = Color.white;
        powerPlant.removeEnergy();
    }
}
