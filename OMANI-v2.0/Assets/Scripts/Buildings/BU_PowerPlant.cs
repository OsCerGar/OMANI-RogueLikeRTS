using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_PowerPlant : BU
{
    [SerializeField]
    private GameObject cablePrefab;

    [SerializeField]
    List<temporalCable> cables = new List<temporalCable>();
    public int energy = 0;

    public MeshRenderer[] buttons;


    //Audios

    AudioSource addedWorker;

    // Use this for initialization
    void Start()
    {

        maxnumberOfWorkers = 4;

        // Searches buttons
        buttons = this.transform.Find("Office").GetChild(0).GetComponentsInChildren<MeshRenderer>();

        foreach (temporalCable cable in this.transform.GetChild(0).GetChild(0).GetComponentsInChildren<temporalCable>())
        {
            cables.Add(cable);
        }

        //Audio
        addedWorker = this.GetComponent<AudioSource>();

        //Adds one energy at the start.
        AddEnergy();

    }

    private void FixedUpdate()
    {
        if (workers.Count > numberOfWorkers)
        {
            int difference = workers.Count - numberOfWorkers;

            for (int i = 0; i < difference; i++)
            {
                AddEnergy();
            }

            numberOfWorkers = workers.Count;
        }

        else if (workers.Count < numberOfWorkers)
        {
            int difference = numberOfWorkers - workers.Count;

            for (int i = 0; i < difference; i++)
            {
                RemoveEnergy();
            }
            numberOfWorkers = workers.Count;
        }

    }

    public void AddEnergy()
    {
        bool givenEnergy = false;
        buttons[energy].material.color = Color.yellow;
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

        addedWorker.Play();
    }

    public void RemoveEnergy()
    {
        bool removedEnergy = false;
        this.energy -= 1;
        buttons[energy].material.color = Color.white;

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
