using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Energy : Interactible
{
    [SerializeField]
    public bool ready { get; set; }
    public NPC npc;

    public override void Start()
    {
        base.Start();
        npc = GetComponent<NPC>();

        price = 75;
        linkPrice = 2;
        finalLinkPrice = 7;
        currentLinkPrice = 0;
        t = 0.2f;
    }

    public override void Action()
    {
        currentLinkPrice = Mathf.Lerp(linkPrice, finalLinkPrice, t);
        t += t * Time.unscaledDeltaTime;

        if (!ready)
        {
            startTime = Time.time;

            if (powers.reducePower(currentLinkPrice))
            {
                npc.powerPool += currentLinkPrice * Time.unscaledDeltaTime;
                actionBool = true;
            }
            else
            {
                actionBool = false;
            }
        }
    }

    public override void FullAction()
    {
        npc.powerPool = powers.reduceAsMuchPower(npc.maxpowerPool);
        laserAudio.energyTransmisionSound(currentLinkPrice);
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
    }

    public override void ActionCompleted()
    {
        startTime = Time.time;
        ready = true;
        currentLinkPrice = 0;
        t = 0.2f;

    }

    public override void ReducePower()
    {
        npc.powerPool -= 1 * Time.unscaledDeltaTime;
    }
}
