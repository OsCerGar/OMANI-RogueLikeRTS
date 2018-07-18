using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_WeaponsMaker : BU_UniqueBuilding
{

    private BU_Building_Action workerMaker;
    private float timeToSpawnWeapons = 45, desiredRotation;
    private float[] timeToSpawnWorkerCounter = new float[3];
    List<Image> weaponsClock = new List<Image>();
    private bool[] weaponsReady = new bool[3];
    private List<GameObject> weaponsPositions = new List<GameObject>();
    [SerializeField]
    private GameObject weapons;
    private GameObject spinningStructure;
    float biggestClockValue;

    public override void Start()
    {
        base.Start();

        workerMaker = this.transform.GetComponentInChildren<BU_Building_Action>();
        foreach (Transform child in this.transform.Find("WeaponsPosition"))
        {
            weaponsPositions.Add(child.gameObject);
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
    public override void Update()
    {
        base.Update();

        if (totalEnergy >= requiredEnergy)
        {
            weaponsMaker();
        }
    }

    public override void BuildingAction()
    {
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

            for (int i = 0; i < weaponsReady.Length; i++)
            {
                if (calcTotalEnergy > 0 && weaponsReady[i] == false)
                {
                    if (timeToSpawnWorkerCounter[i] < timeToSpawnWeapons)
                    {
                        timeToSpawnWorkerCounter[i] += Time.deltaTime;

                        WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWeapons, i, Color.green);
                    }
                    if (timeToSpawnWorkerCounter[i] > timeToSpawnWeapons)
                    {
                        weaponsReady[i] = true;
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
            if (weaponsReady[i])
            {
                //Should be a pool later on
                Instantiate(weapons, weaponsPositions[i].transform.position, Quaternion.identity);
                weaponsReady[i] = false;
                timeToSpawnWorkerCounter[i] = 0;
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
