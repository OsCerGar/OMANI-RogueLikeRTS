using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Powers : MonoBehaviour
{

    List<Image> clocks = new List<Image>();
    Powers powers;
    Camera mainCamera;

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

        for (int i = 0; i < clocks.Count; i++)
        {
            clocks[i].fillAmount = powers.powerPool / powers.maxpowerPool;
            clocks[i].transform.LookAt(mainCamera.transform);
        }
    }
}
