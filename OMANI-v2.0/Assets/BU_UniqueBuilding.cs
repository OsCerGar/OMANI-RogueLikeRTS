using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_UniqueBuilding : MonoBehaviour
{

    public int lastTotalEnergy, totalEnergy, requiredEnergy;


    [SerializeField]
    public BU_Plug[] plugs;

    public List<MeshRenderer> plugMaterial = new List<MeshRenderer>();


    // Use this for initialization
    public virtual void Start()
    {
        //Makes sure it checks for energy on the first run.
        lastTotalEnergy = 100;
        plugs = this.transform.GetChild(0).GetComponentsInChildren<BU_Plug>();


        foreach (BU_Plug plug in plugs)
        {
            plugMaterial.Add(plug.gameObject.GetComponent<MeshRenderer>());
        }

    }

    public virtual void Update()
    {
        totalEnergy = 0;


        foreach (BU_Plug plug in plugs)
        {
            totalEnergy += plug.energy;
        }

        //Checks if there have been changes in the plugs
        if (totalEnergy != lastTotalEnergy)
        {
            if (requiredEnergy > totalEnergy)
            {
                TurnToRed();
            }
            else
            {
                TurnToWhite();
            }
        }

        lastTotalEnergy = totalEnergy;

    }

    public void TurnToRed()
    {
        int redPlugs = 0;
        int energyDifference = requiredEnergy - totalEnergy;

        foreach (MeshRenderer plugMaterials in plugMaterial)
        {
            if (plugMaterials.material.color == Color.red)
            {
                redPlugs++;
            }
        }

        foreach (MeshRenderer plugMaterials in plugMaterial)
        {
            if (redPlugs < energyDifference)
            {
                if (plugMaterials.material.color == Color.white)
                {
                    plugMaterials.material.color = Color.red;
                    redPlugs++;
                }
            }
        }
    }


    public void TurnToAllRed()
    {
        foreach (MeshRenderer plugMaterials in plugMaterial)
        {
            if (plugMaterials.material.color == Color.white)
            {
                plugMaterials.material.color = Color.red;
            }

        }
    }

    public void TurnToWhite()
    {
        foreach (MeshRenderer plugMaterials in plugMaterial)
        {
            if (plugMaterials.material.color != Color.yellow)
            {
                plugMaterials.material.color = Color.white;
            }
        }
    }


}
