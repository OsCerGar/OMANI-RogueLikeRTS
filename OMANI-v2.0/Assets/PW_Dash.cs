using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PW_Dash : Power
{

    float regularSpeed, dashSpeed, startTime, timer, timerSpeed = 0.2f, endTimer = 2.5f, energyCost = 10;
    bool dashin, normalValuesDone;
    public override void Awake()
    {
        base.Awake();

        regularSpeed = player.speed;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (dashin)
        {
            timer += Time.fixedDeltaTime;

            if (dashSpeed > regularSpeed)
            {
                dashSpeed -= timer * timerSpeed;
                player.speed = dashSpeed;
            }

            if (dashSpeed < regularSpeed)
            {
                player.speed = regularSpeed;

                if (!normalValuesDone)
                {
                    locomotionBrain.normalValues();
                    normalValuesDone = true;
                }
            }

            if (timer > endTimer)
            {
                timer = 0;
                dashin = false;
            }
        }
    }

    public void Dash()
    {

        if (!dashin)
        {
            if (powers.reducePower(energyCost))
            {
                dashSpeed = player.speed * 5;
                startTime = Time.time;
                dashin = true;
                normalValuesDone = false;
                locomotionBrain.DashValues();
            }
        }
    }
}
