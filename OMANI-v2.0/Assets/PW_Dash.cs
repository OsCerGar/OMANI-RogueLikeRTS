using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PW_Dash : Power
{

    float regularSpeed, runningSpeed, energyCost = 7;
    bool runnin;
    public override void Awake()
    {
        base.Awake();
        regularSpeed = player.speed;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (runnin)
        {
            if (!powers.reducePower(energyCost))
            {
                StopRunning();
            }
        }
    }

    public override void CastPower()
    {
        Running();
    }

    public void Running()
    {
        if (!runnin)
        {
            runnin = true;
            runningSpeed = player.speed * 1.5f;
            player.speed = runningSpeed;
        }
    }

    public void StopRunning()
    {
        runnin = false;
        player.speed = regularSpeed;
    }
}
