using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Energy : Interactible
{
    public bool ready { get; set; }
    private float startTime;
    public NPC npc;

    public override void Start()
    {
        base.Start();
        npc = GetComponent<NPC>();
        price = 25;
    }

    public override void Action()
    {
        if (!ready)
        {
            startTime = Time.time;

            if (powers.reducePower(linkPrice))
            {
                npc.powerPool += linkPrice * Time.unscaledDeltaTime;
                actionBool = true;
            }
            else
            {
                actionBool = false;
            }
        }
    }

    public override void Update()
    {
        if (npc.powerPool >= npc.maxpowerPool)
        {
            ActionCompleted();
        }
        else
        {
            ready = false;
        }

        if (!ready)
        {
            if (npc.getState() != "Follow")
            {
                if (Time.time - startTime > 3f)
                {
                    npc.reducePower(1);
                }
            }
        }

        else
        {
            if (npc.getState() != "Follow")
            {
                if (Time.time - startTime > 10f)
                {
                    ready = false;
                    npc.reducePower(1);
                }
            }
        }
    }

    public override void ActionCompleted()
    {
        Debug.Log("true");
        startTime = Time.time;
        ready = true;
    }

    public override void ReducePower()
    {
        Debug.Log("reducepower");
        npc.powerPool -= 1 * Time.unscaledDeltaTime;
    }
}
