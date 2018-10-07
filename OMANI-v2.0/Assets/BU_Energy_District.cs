using System.Collections.Generic;
using UnityEngine;

public class BU_Energy_District : MonoBehaviour
{

    private List<Interactible_Repeater> repeaters = new List<Interactible_Repeater>();

    private void Start()
    {
        repeaters.Add(transform.Find("MainRepeater").gameObject.GetComponent<Interactible_Repeater>());
        repeaters.Add(transform.Find("Repeater1").gameObject.GetComponent<Interactible_Repeater>());
        repeaters.Add(transform.Find("Repeater2").gameObject.GetComponent<Interactible_Repeater>());
    }

    public List<Interactible_Repeater> returnRepeatersEnergy()
    {
        return repeaters;
    }

    public int returnEnergyFull()
    {
        int total = repeaters[0].energy + repeaters[1].energy + repeaters[2].energy;
        return total;
    }
    public int returnEnergyWithoutMain()
    {
        int total = repeaters[1].energy + repeaters[2].energy;
        return total;
    }

}
