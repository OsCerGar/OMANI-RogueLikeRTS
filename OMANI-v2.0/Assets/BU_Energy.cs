using EZObjectPools;
using System.Collections.Generic;
using UnityEngine;

public class BU_Energy : BU_UniqueBuilding
{
    public int energy = 3, usedEnergy = 0;

    GameObject top;
    EZObjectPool cables;
    GameObject Spawned;

    //Interactible repeaters
    public List<Interactible_Repeater> centerRepeaters = new List<Interactible_Repeater>();
    public List<Interactible_Repeater> currentRepeaters = new List<Interactible_Repeater>();
    public List<CableComponent> cablesOut = new List<CableComponent>();
    public MeshRenderer[] buttons;

    //Animations
    float animationValue;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        top = transform.Find("Top").gameObject;

        // Searches buttons
        buttons = transform.Find("Buttons").GetComponentsInChildren<MeshRenderer>();

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

    private void closeRepeatersAvailable()
    {
        if (currentRepeaters.Count == 0)
        {
            foreach (Interactible_Repeater repeater in centerRepeaters)
            {
                repeater.Availablity(true);
            }
        }
    }
    private void closeRepeatersNotAvailable()
    {
        foreach (Interactible_Repeater repeater in centerRepeaters)
        {
            bool currentRp = false;

            foreach (Interactible_Repeater currentRepeater in currentRepeaters)
            {
                if (currentRepeater == repeater)
                {
                    currentRp = true;
                }
            }

            if (!currentRp)
            {
                repeater.Availablity(false);
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
    public bool checkIfLastRepeater(Interactible_Repeater _repeater)
    {
        if (currentRepeaters.Count > 0)
        {
            if (currentRepeaters[currentRepeaters.Count - 1] == _repeater)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {

            return false;
        }
    }

    public bool RequestCable(GameObject _position, Interactible_Repeater repeater)
    {
        //ifenergyright
        if (energyCheck())
        {
            if (currentRepeaters.Count > 0)
            {
                //disables nearby repeaters
                //currentRepeaters[currentRepeaters.Count - 1].closeRepeatersOnOff(false);
                LaunchCableFromRepeater(_position);
                currentRepeaters.Add(repeater);
            }

            else
            {
                closeRepeatersNotAvailable();
                currentRepeaters.Add(repeater);
                LaunchCable(_position);
            }

            buttons[usedEnergy].material.color = Color.yellow;
            usedEnergy++;


            //if you don't have energy for more, doesnt make other repeaters Available
            if (currentRepeaters.Count == energy)
            {
                repeater.closeRepeatersOnOff(false);
            }
            else
            {
                repeater.closeRepeatersOnOff(true);
            }

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
                currentRepeaters[currentRepeaters.Count - 1].closeRepeatersOnOff(false);
                currentRepeaters.Remove(currentRepeaters[currentRepeaters.Count - 1]);

                cablesOut[i].transform.position = top.transform.position;
                cablesOut[i].cableEnd.PullBack();
                cablePulled();
                cablesOut.Remove(cablesOut[i]);
            }
        }

        closeRepeatersAvailable();

        if (currentRepeaters.Count > 0)
        {
            currentRepeaters[currentRepeaters.Count - 1].closeRepeatersOnOff(true);
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
        cabl.cableEnd.LaunchVector3(_position.transform, true);
        cablesOut.Add(cabl);
    }

    private void LaunchCableFromRepeater(GameObject _position)
    {
        cables.TryGetNextObject(currentRepeaters[currentRepeaters.Count - 1].top.transform.position, currentRepeaters[currentRepeaters.Count - 1].top.transform.rotation, out Spawned);
        CableComponent cabl = Spawned.GetComponent<CableComponent>();
        cabl.cableEnd.LaunchVector3(_position.transform, true);
        cablesOut.Add(cabl);
    }

    private void TakeCablesBack()
    {
        for (int i = 0; i < cablesOut.Count; i++)
        {

            currentRepeaters[currentRepeaters.Count - 1].closeRepeatersOnOff(false);
            currentRepeaters.Remove(currentRepeaters[currentRepeaters.Count - 1]);
            cablesOut[i].transform.position = top.transform.position;
            cablesOut[i].cableEnd.PullBackStopWorking();
            cablePulled();

        }
        cablesOut.Clear();

        closeRepeatersAvailable();
    }

    public override void BuildingAction()
    {
        base.BuildingAction();
        TakeCablesBack();

    }
}
