using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_UI : MonoBehaviour
{

    public Image powerClock, lifeClock;
    public NPC npc;
    float lastPowerPool, lastLife, powerTimer;
    bool powerClockHidden, lifeClockHidden;
    public Quaternion fixedRotation;
    
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
        fixedRotation = lifeClock.transform.rotation;
        npc = this.transform.GetComponentInParent<NPC>();
    }

    // Update is called once per frame
    public virtual void LateUpdate()
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

        //Energy bar dissapears
        /*
        else
        {
            if (npc.getState().Equals("Idle"))
            {
                if (!powerClockHidden)
                {
                    powerTimer = Time.time;
                    powerClockHidden = true;
                }
                if (Time.time - powerTimer > 5f)
                {
                    powerClock.enabled = false;
                }
            }
        }
        */
        if (lastLife != npc.life)
        {
            lifeClockHidden = false;
            lifeClock.enabled = true;
            lifeClock.fillAmount = ((float)npc.life / (float)npc.startLife);
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
