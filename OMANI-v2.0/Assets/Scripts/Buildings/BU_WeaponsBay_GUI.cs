using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BU_WeaponsBay_GUI : MonoBehaviour
{

    Image spawnClock;
    Image energyClock;

    private void Start()
    {
        spawnClock = this.transform.Find("BU_UI_Equipment").GetChild(0).GetChild(0).GetComponent<Image>();
        energyClock = this.transform.Find("BU_UI_Energy").GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void ChangeEquipmentClock(float _fillAmount)
    {
        spawnClock.fillAmount = _fillAmount;

    }
    public void ChangeEnergyClock(float _fillAmount)
    {
        energyClock.fillAmount = _fillAmount;

    }
    public void ChangeEnergyColor(Color _color)
    {
        energyClock.color = _color;
    }
}
