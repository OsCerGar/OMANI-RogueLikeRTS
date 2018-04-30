using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_Healing_GUI : MonoBehaviour
{

    Image healingClock;

    private void Start()
    {
        healingClock = this.transform.Find("BU_UI_Healing").GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void ChangeHealingClock(float _fillAmount)
    {
        healingClock.fillAmount = _fillAmount;
    }
}
