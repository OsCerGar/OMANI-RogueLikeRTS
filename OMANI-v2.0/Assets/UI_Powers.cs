using UnityEngine;
using UnityEngine.UI;

public class UI_Powers : MonoBehaviour
{

    public Image powerClock, lifeClock;
    Powers powers;
    Player player;
    Camera mainCamera;
    float lastPowerPool, lastLife;
    bool powerClockHidden, lifeClockHidden;
    Quaternion fixedRotation;

    // Use this for initialization
    void Start()
    {
        foreach (Image ui_clock in transform.GetComponentsInChildren<Image>())
        {
            if (ui_clock.name == "PowerClock")
            {
                Image realClock = ui_clock.transform.GetChild(0).GetComponent<Image>();
                powerClock = realClock;
                fixedRotation = powerClock.transform.rotation;

            }
            if (ui_clock.name == "LifeClock")
            {
                //Image realClock = ui_clock.transform.GetChild(0).GetComponent<Image>();
                //lifeClock = realClock;
            }
        }
        player = transform.root.GetComponentInChildren<Player>();
        powers = transform.root.GetComponentInChildren<Powers>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {/*
        if (lastPowerPool != powers.powerPool)
        {
            powerClockHidden = false;
            powerClock.enabled = true;
            powerClock.fillAmount = powers.powerPool / powers.maxpowerPool;
            lastPowerPool = powers.powerPool;
            //Restores rotation
            powerClock.transform.rotation = fixedRotation;

        }
        else
        {
            if (!powerClockHidden)
            {
                powerClock.enabled = false;

                powerClockHidden = true;
            }
        }
        /*
        if (lastLife != player.life)
        {
            lifeClockHidden = false;
            lifeClock.enabled = true;
            lifeClock.fillAmount = ((float)player.life / (float)player.startLife);
            lastLife = player.life;
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
        */
    }
}
