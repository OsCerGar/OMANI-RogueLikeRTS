using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_WeaponsMaker : BU_UniqueBuilding
{
    private float timeToSpawnWeapons = 45, desiredRotation;
    private float[] timeToSpawnWorkerCounter = new float[3];
    List<Image> weaponsClock = new List<Image>();

    private List<BU_Cabin> weaponsCabins = new List<BU_Cabin>();

    bool atleastOneWorkerInside;
    [SerializeField]
    private GameObject weapons;
    private GameObject spinningStructure;
    float biggestClockValue;

    public override void Start()
    {
        base.Start();

        buildingActionMesh = this.transform.GetComponentInChildren<BU_Building_Action>();

        foreach (BU_Cabin child in this.transform.Find("Cabins").gameObject.GetComponentsInChildren<BU_Cabin>())
        {
            weaponsCabins.Add(child);
        }

        foreach (Image clock in this.transform.Find("BU_UI/Production_Clocks").GetComponentsInChildren<Image>())
        {
            if (clock.name == "Clock")
            {
                weaponsClock.Add(clock);
            }
        }

        requiredEnergy = 1;
    }

    // Update is called once per frame
    public void Update()
    {
        if (totalEnergy >= requiredEnergy)
        {
            weaponsMaker();
        }

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

    }

    public override void BuildingAction()
    {
        base.BuildingAction();
        ShowWeapons();
    }

    //Makes Workers
    private void weaponsMaker()
    {
        //Checks energy up to 3 to see how much it creates. Sends info to the clocks with @WorkerClocks.
        if (totalEnergy > 0)
        {
            //Used to see how many workers are going to be build.
            int calcTotalEnergy = totalEnergy;

            for (int i = 0; i < weaponsCabins.Count; i++)
            {
                if (calcTotalEnergy > 0 && weaponsCabins[i].ready == false)
                {
                    if (timeToSpawnWorkerCounter[i] < timeToSpawnWeapons)
                    {
                        timeToSpawnWorkerCounter[i] += Time.deltaTime;

                        WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWeapons, i, Color.green);
                    }
                    if (timeToSpawnWorkerCounter[i] > timeToSpawnWeapons)
                    {
                        weaponsCabins[i].CabinReady();
                        WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWeapons, i, Color.cyan);
                    }
                    calcTotalEnergy -= 1;
                }
            }
        }
    }

    public void ShowWeapons()
    {
        for (int i = 0; i < 3; i++)
        {
            if (weaponsCabins[i].workerInside)
            {
                weaponsCabins[i].TurnWorker();
                timeToSpawnWorkerCounter[i] = 0;
                atleastOneWorkerInside = false;
                //restart
                biggestClockValue = 0;
                WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWeapons, i, Color.green);
            }
        }
    }

    private float biggestClock()
    {
        for (int i = 0; i < 3; i++)
        {
            if (timeToSpawnWorkerCounter[i] / timeToSpawnWeapons > biggestClockValue) { biggestClockValue = timeToSpawnWorkerCounter[i] / timeToSpawnWeapons; }
        }
        return biggestClockValue;
    }

    private void WorkerClocks(float _fillAmount, int _clock, Color _color)
    {

        weaponsClock[_clock].fillAmount = _fillAmount;
        weaponsClock[_clock].color = _color;

    }
}
