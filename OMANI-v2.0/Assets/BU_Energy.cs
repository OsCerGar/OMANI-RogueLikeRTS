using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class BU_Energy : MonoBehaviour
{
    public int energy = 3, usedEnergy = 0;

    GameObject top;
    EZObjectPool cables;
    GameObject Spawned;

    List<CableComponent> cablesOut = new List<CableComponent>();
    public MeshRenderer[] buttons;

    // Use this for initialization
    void Start()
    {
        top = this.transform.Find("Top").gameObject;
        // Searches buttons
        buttons = this.transform.Find("Buttons").GetComponentsInChildren<MeshRenderer>();

        // Searches buttons
        //buttons = this.transform.Find("Office").GetChild(0).GetComponentsInChildren<MeshRenderer>();

        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "Cables")
            {
                cables = item;
            }
        }

    }

    public bool energyCheck()
    {
        if (usedEnergy < energy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool RequestCable(GameObject _position)
    {
        //ifenergyright
        if (energyCheck())
        {
            LaunchCable(_position);
            buttons[usedEnergy].material.color = Color.yellow;
            usedEnergy++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void pullBackCable(Transform repeater)
    {

        for (int i = 0; i < cablesOut.Count; i++)
        {
            if (cablesOut[i].cableEnd.destination == repeater)
            {
                cablesOut[i].cableEnd.PullBack();
                cablePulled();
                cablesOut.Remove(cablesOut[i]);
            }
        }
    }

    public void cablePulled()
    {
        //Removes energy from being used at the building.
        usedEnergy--;
        buttons[usedEnergy].material.color = Color.white;
    }

    private void LaunchCable(GameObject _position)
    {
        cables.TryGetNextObject(top.transform.position, top.transform.rotation, out Spawned);
        CableComponent cabl = Spawned.GetComponent<CableComponent>();
        cabl.cableEnd.Launch(_position.transform, true);
        cablesOut.Add(cabl);
    }

}
