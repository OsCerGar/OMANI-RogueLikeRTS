using System.Collections.Generic;
using UnityEngine;

public class BU_WeaponsMaker : BU_UniqueBuilding
{

    private List<BU_Cabin> weaponsCabins = new List<BU_Cabin>();

    bool atleastOneWorkerInside;
    [SerializeField]
    private GameObject weapons;
    private GameObject spinningStructure;

    BU_WeaponsMaker_Animation animator;


    public override void Start()
    {
        base.Start();
        animator = transform.Find("Animations").GetComponent<BU_WeaponsMaker_Animation>();

        buildingActionMesh = transform.GetComponentInChildren<BU_Building_Action>();

        foreach (BU_Cabin child in transform.Find("Cabins").gameObject.GetComponentsInChildren<BU_Cabin>())
        {
            weaponsCabins.Add(child);
        }

        requiredEnergy = 25;
    }

    // Update is called once per frame
    public void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (weaponsCabins[i].workerInside)
            {
                atleastOneWorkerInside = true;
            }
        }

        if (atleastOneWorkerInside)
        {
            buildingActionMesh.readyToSpawn = true;
        }
        else
        {
            buildingActionMesh.readyToSpawn = false;
        }

        if (buildingDistrict.totalEnergyReturn() > requiredEnergy) { weaponsMaker(); }

    }

    public void LateUpdate()
    {
        animator.buildingAnimations(totalEnergy);
    }

    public override void BuildingAction()
    {
        base.BuildingAction();
        ShowWeapons();
    }

    //Makes Workers
    private void weaponsMaker()
    {
        weaponsCabins[0].CabinReady();
    }

    public void ShowWeapons()
    {
        for (int i = 0; i < 3; i++)
        {
            if (weaponsCabins[i].workerInside)
            {
                weaponsCabins[0].TurnWorker();
                atleastOneWorkerInside = false;
                //restart
            }
        }
    }
}
