
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_Energy_GUI : MonoBehaviour
{

    Image energyClock;

    private void Start()
    {
        energyClock = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void ChangeClock(float _fillAmount)
    {
        energyClock.fillAmount = _fillAmount;

    }
}
