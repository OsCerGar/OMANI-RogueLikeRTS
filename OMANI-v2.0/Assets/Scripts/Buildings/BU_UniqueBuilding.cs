using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_UniqueBuilding : MonoBehaviour
{
    public int lastTotalEnergy { get; set; }
    public int totalEnergy { get; set; }
    public int requiredEnergy { get; set; }
    public BU_Building_Action workerMaker;

    [SerializeField]
    public Interactible_Repeater[] plugs { get; set; }
    // Use this for initialization
    public virtual void Start()
    {
        //Makes sure it checks for energy on the first run.
        lastTotalEnergy = 100;

        plugs = this.transform.Find("Electricity").GetComponentsInChildren<Interactible_Repeater>();
        workerMaker = this.transform.GetComponentInChildren<BU_Building_Action>();

    }

    //THIS SHOULDN'T BE IN AN UPDATE
    public virtual void LateUpdate()
    {
        EnergyCalc();
    }

    private void EnergyCalc()
    {
        totalEnergy = 0;

        foreach (Interactible_Repeater plug in plugs)
        {
            if (plug.energy == true)
            {
                totalEnergy++;
            }
        }

        lastTotalEnergy = totalEnergy;
    }

    public virtual void BuildingAction()
    {

    }
}
