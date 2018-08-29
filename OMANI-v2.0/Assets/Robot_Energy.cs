using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Energy : Interactible
{
    public bool ready { get; set; }
    private float startTime;

    public override void Start()
    {
        base.Start();

        price = 25;
    }

    public override void Action()
    {
        if (!ready) { base.Action(); }
    }

    public override void Update()
    {
        if (!ready)
        {
            if (Time.time - startTime > 3f && powerReduced > 0)
            {
                ReducePower();
            }
        }
        else
        {
            if (Time.time - startTime > 10f && powerReduced > 0)
            {
                ready = false;
                ReducePower();
            }

        }
    }

    public override void ActionCompleted()
    {
        startTime = Time.time;
        ready = true;
    }

}
