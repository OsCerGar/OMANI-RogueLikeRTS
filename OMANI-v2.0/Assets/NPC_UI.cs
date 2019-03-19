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
        foreach (Image ui_clock in transform.GetComponentsInChildren<Image>())
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
                fixedRotation = lifeClock.transform.rotation;
            }

        }
        npc = transform.GetComponentInParent<NPC>();
        powerClock.enabled = true;

    }

    // Update is called once per frame
    public virtual void LateUpdate()
    {
        powerClock.fillAmount = npc.powerPool / npc.maxpowerPool;
        //lastPowerPool = npc.powerPool;
        //Restores rotation
        powerClock.transform.rotation = fixedRotation;

    }
}


