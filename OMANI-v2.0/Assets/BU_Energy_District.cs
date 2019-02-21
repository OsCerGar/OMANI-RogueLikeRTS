using System.Collections.Generic;
using UnityEngine;

public class BU_Energy_District : MonoBehaviour
{

    private List<Interactible_Repeater> repeaters = new List<Interactible_Repeater>();
    private Interactible_Repeater MainRepeater, repeater1, repeater2;
    private void Start()
    {
        MainRepeater = transform.Find("MainRepeater").gameObject.GetComponent<Interactible_Repeater>();
        repeater1 = transform.Find("Repeater1").gameObject.GetComponent<Interactible_Repeater>();
        repeater2 = transform.Find("Repeater2").gameObject.GetComponent<Interactible_Repeater>();

        repeaters.Add(MainRepeater);
        repeaters.Add(repeater1);
        repeaters.Add(repeater2);
    }

    public List<Interactible_Repeater> returnRepeatersEnergy()
    {
        return repeaters;
    }

    public int returnEnergyFull()
    {
        int total = 0;
        if (MainRepeater != null)
        {
            total += MainRepeater.energy;
        }
        if (repeater1 != null)
        {
            total += repeater1.energy;
        }
        if (repeater2 != null)
        {
            total += repeater2.energy;
        }

        return total;
    }
    public int returnEnergyWithoutMain()
    {
        int total = 0;
        if (repeater1 != null)
        {
            total += repeater1.energy;
        }
        if (repeater2 != null)
        {
            total += repeater2.energy;
        }

        return total;
    }

}
