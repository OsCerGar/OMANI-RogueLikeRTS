using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.UI;

public class BU_Building_Ennui : BU_UniqueBuilding
{

    EnnuiSpawnerManager EnnuiPool;
    Transform EnnuiSpawn;

    private bool ennuisReady;
    private float timeToSpawnEnnui = 120;
    private float ennuisToSpawn = 20;
    private float timeToSpawnEnnuiCounter, biggestClockValue;
    Image ennuiClocks;

    public override void Start()
    {
        base.Start();
        EnnuiPool = FindObjectOfType<EnnuiSpawnerManager>();
        EnnuiSpawn = this.transform.Find("EnnuiSpawner");

        foreach (Image clock in this.transform.Find("BU_UI/Production_Clocks").GetComponentsInChildren<Image>())
        {
            if (clock.name == "Clock")
            {
                ennuiClocks = clock;
            }
        }

        requiredEnergy = 1;
    }

    public void Update()
    {
        if (totalEnergy >= requiredEnergy)
        {
            EnnuiMaker();
        }
        if (timeToSpawnEnnuiCounter > 0)
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
        SpitEnnuis();
    }

    private void SpitEnnuis()
    {
        int ennuisToSpawnFinal = Mathf.RoundToInt((timeToSpawnEnnuiCounter / timeToSpawnEnnui) * ennuisToSpawn);
        timeToSpawnEnnuiCounter = 0;
        ennuisReady = false;
        for (int i = 0; i < ennuisToSpawnFinal; i++)
        {
            EnnuiPool.SpawnEnnuiParabola(EnnuiSpawn);
        }
    }

    private void EnnuiMaker()
    {
        //Checks energy up to 3 to see how much it creates. Sends info to the clocks with @WorkerClocks.
        if (totalEnergy > 0)
        {
            //Used to see how many workers are going to be build.
            int calcTotalEnergy = totalEnergy;

            if (calcTotalEnergy > 0 && ennuisReady == false)
            {
                for (int i = 0; i < calcTotalEnergy; i++)
                {
                    if (timeToSpawnEnnuiCounter < timeToSpawnEnnui)
                    {
                        timeToSpawnEnnuiCounter += Time.deltaTime;

                        EnnuiClocks(timeToSpawnEnnuiCounter / timeToSpawnEnnui, Color.green);
                    }
                    if (timeToSpawnEnnuiCounter > timeToSpawnEnnui)
                    {
                        ennuisReady = true;
                        EnnuiClocks(timeToSpawnEnnuiCounter / timeToSpawnEnnui, Color.cyan);
                    }
                }

                calcTotalEnergy -= 1;
            }

        }

    }

    private void EnnuiClocks(float _fillAmount, Color _color)
    {

        ennuiClocks.fillAmount = _fillAmount;
        ennuiClocks.color = _color;
    }
}
