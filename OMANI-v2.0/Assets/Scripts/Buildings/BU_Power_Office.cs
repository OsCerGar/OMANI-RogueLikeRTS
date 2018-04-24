using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Power_Office : MonoBehaviour
{

    public BoxCollider myBoxCollider;
    public MeshRenderer[] buttons;
    int thisEnergy;
    BU_PowerPlant powerPlant;

    private void Awake()
    {
        buttons = this.transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
        powerPlant = this.transform.parent.GetComponent<BU_PowerPlant>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People") && thisEnergy < buttons.Length)
        {
            if (other.GetComponent<Player>() == null)
            {
                AddEnergy();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("People") && thisEnergy > 0)
        {
            if (other.GetComponent<Player>() == null)
            {
                RemoveEnergy();
            }
        }
    }

    void AddEnergy()
    {
        powerPlant.AddEnergy();
        buttons[thisEnergy].material.color = Color.yellow;
        thisEnergy = thisEnergy + 1;
    }

    void RemoveEnergy()
    {
        thisEnergy = thisEnergy - 1;
        buttons[thisEnergy].material.color = Color.white;
        powerPlant.RemoveEnergy();
    }
}
