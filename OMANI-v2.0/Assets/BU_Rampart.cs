using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Rampart : BU_UniqueBuilding
{
    Turret[] turrets;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        maxnumberOfWorkers = 0;
        requiredEnergy = 1;

        turrets = this.transform.Find("Turrets").GetComponentsInChildren<Turret>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        switch (totalEnergy)
        {
            case 0:
                turrets[0].energy = false;
                turrets[1].energy = false;

                break;
            case 1:
                turrets[0].energy = true;
                turrets[1].energy = false;

                break;
            case 2:
                turrets[0].energy = true;
                turrets[1].energy = true;
                break;
        }
    }
}
