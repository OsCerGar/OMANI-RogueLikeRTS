using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class BU_Energy : MonoBehaviour
{
    public int energy = 3, usedEnergy = 0;

    public MeshRenderer[] buttons;

    GameObject top;
    EZObjectPool cables;
    GameObject Spawned;

    List<CableComponent> cablesOut = new List<CableComponent>();

    // Use this for initialization
    void Start()
    {
        top = this.transform.Find("Top").gameObject;

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
        if (usedEnergy < energy)
        {
            LaunchCable(_position);
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
                cablesOut.Remove(cablesOut[i]);
            }
        }
    }

    private void LaunchCable(GameObject _position)
    {
        cables.TryGetNextObject(top.transform.position, top.transform.rotation, out Spawned);
        CableComponent cabl = Spawned.GetComponent<CableComponent>();
        cabl.cableEnd.Launch(_position.transform, true);
        cablesOut.Add(cabl);
    }

}
