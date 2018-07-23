using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PW_Dash : Power
{

    float regularSpeed, dashSpeed, startTime, timer, timerSpeed = 2;
    bool dashin;
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
            dashSpeed -= timer * timerSpeed;
            player.speed = dashSpeed;

            if (dashSpeed < regularSpeed)
            {
                timer = 0;
                dashin = false;
                player.speed = regularSpeed;

            }
        }
    }

    public void Dash()
    {
        if (!dashin)
        {
            dashSpeed = player.speed * 10;
            startTime = Time.time;
            dashin = true;
        }
    }
}
