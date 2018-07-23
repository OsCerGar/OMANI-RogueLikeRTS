using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Powers : MonoBehaviour
{

    List<Image> clocks = new List<Image>();
    Powers powers;
    Camera mainCamera;
    float lastPowerPool;
    bool clocksHidden;

    // Use this for initialization
    void Start()
    {
        foreach (Image ui_clock in this.transform.GetComponentsInChildren<Image>())
        {
            if (ui_clock.name == "PowerClock")
            {
                Image realClock = ui_clock.transform.GetChild(0).GetComponent<Image>();
                clocks.Add(realClock);
            }
        }
        powers = this.transform.root.GetComponentInChildren<Powers>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPowerPool != powers.powerPool)
        {
            clocksHidden = false;
            for (int i = 0; i < clocks.Count; i++)
            {
                clocks[i].enabled = true;
                clocks[i].fillAmount = powers.powerPool / powers.maxpowerPool;
                clocks[i].transform.LookAt(mainCamera.transform);
                lastPowerPool = powers.powerPool;
            }
        }
        else
        {
            if (!clocksHidden)
            {
                for (int i = 0; i < clocks.Count; i++)
                {
                    clocks[i].enabled = false;
                }
                clocksHidden = true;
            }
        }
    }
}
