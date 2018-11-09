using UnityEngine;
using UnityEngine.UI;

public class BU_Building_Ennui : BU_UniqueBuilding
{

    EnnuiSpawnerManager EnnuiPool;
    Transform EnnuiSpawn;

    private bool ennuisReady;
    private float ennuisToSpawn = 5;
    private float timeToSpawnEnnuiCounter, biggestClockValue;
    Image ennuiClocks;

    public override void Start()
    {
        base.Start();
        EnnuiPool = FindObjectOfType<EnnuiSpawnerManager>();
        EnnuiSpawn = transform.Find("EnnuiSpawner");

        foreach (Image clock in transform.Find("BU_UI/Production_Clocks").GetComponentsInChildren<Image>())
        {
            if (clock.name == "Clock")
            {
                ennuiClocks = clock;
            }
        }

        requiredEnergy = 25;
    }

    public override void BuildingAction()
    {
        base.BuildingAction();
        SpitEnnuis();
    }

    private void SpitEnnuis()
    {
        for (int i = 0; i < ennuisToSpawn; i++)
        {
            EnnuiPool.SpawnEnnuiParabola(EnnuiSpawn);
        }
    }
}
