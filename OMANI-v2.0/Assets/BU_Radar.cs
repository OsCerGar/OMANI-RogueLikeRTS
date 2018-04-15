using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Radar : BU_UniqueBuilding
{

    Radar radar;

    public override void Start()
    {
        base.Start();
        requiredEnergy = 1;
        radar = GetComponentInChildren<Radar>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (requiredEnergy > totalEnergy)
        {
            radar.RemoveAllBlips();
            radar.enabled = false;
        }
        else
        {
            radar.enabled = true;
        }

    }
}
