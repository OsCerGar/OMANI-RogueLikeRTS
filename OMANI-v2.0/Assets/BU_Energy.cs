using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Energy : MonoBehaviour
{
    [SerializeField]
    private GameObject cablePrefab;

    [SerializeField]
    List<CableComponent> cables = new List<CableComponent>();
    public int energy = 0, usedEnergy = 0;

    public MeshRenderer[] buttons;

    GameObject top;

    // Use this for initialization
    void Start()
    {
        top = this.transform.Find("Top").gameObject;

        // Searches buttons
        //buttons = this.transform.Find("Office").GetChild(0).GetComponentsInChildren<MeshRenderer>();

        foreach (CableComponent cable in this.transform.Find("Cables").GetComponentsInChildren<CableComponent>())
        {
            cables.Add(cable);
            energy++;
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
        foreach (CableComponent cable in cables)
        {
            if (cable.cableEnd.destination == repeater)
            {
                cable.cableEnd.PullBack();
                usedEnergy--;
            }
        }
    }

    private void LaunchCable(GameObject _position)
    {
        cables[usedEnergy].cableEnd.Launch(_position.transform, true);
    }
}
