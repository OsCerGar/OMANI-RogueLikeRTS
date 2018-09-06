using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_UI : MonoBehaviour
{

    public Image powerClock, lifeClock;
    NPC npc;
    float lastPowerPool, lastLife, powerTimer;
    bool powerClockHidden, lifeClockHidden;
    Quaternion fixedRotation;

    // Use this for initialization
    void Start()
    {
        foreach (Image ui_clock in this.transform.GetComponentsInChildren<Image>())
        {
            if (ui_clock.name == "PowerClock")
            {
                Image realClock = ui_clock.transform.GetChild(0).GetComponent<Image>();
                powerClock = realClock;
            }
            if (ui_clock.name == "LifeClock")
            {
                Image realClock = ui_clock.transform.GetChild(0).GetComponent<Image>();
                lifeClock = realClock;
            }
        }
        fixedRotation = powerClock.transform.rotation;
        npc = this.transform.GetComponentInParent<NPC>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (lastPowerPool != npc.powerPool)
        {
            powerClockHidden = false;
            powerClock.enabled = true;
            powerClock.fillAmount = npc.powerPool / npc.maxpowerPool;
            lastPowerPool = npc.powerPool;
            //Restores rotation
            powerClock.transform.rotation = fixedRotation;

        }
        else
        {
            if (!powerClockHidden)
            {
                powerTimer = Time.time;
                powerClockHidden = true;
            }
            if (Time.time - powerTimer > 3f)
            {
                powerClock.enabled = false;
            }
        }
        if (lastLife != npc.life)
        {
            lifeClockHidden = false;
            lifeClock.enabled = true;
            lifeClock.fillAmount = npc.life / npc.startLife;
            lastLife = npc.life;
            //Restores rotation
            lifeClock.transform.rotation = fixedRotation;

        }
        else
        {
            if (!lifeClockHidden)
            {
                lifeClock.enabled = false;

                lifeClockHidden = true;
            }
        }
    }
}
