using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_WeaponsBay : MonoBehaviour
{

    [SerializeField ]
    Component buildingTypeAndBehaviour;


    public int totalEnergy, requiredEnergy;

    [SerializeField]
    BU_Plug[] plugs;

    // Use this for initialization
    void Start()
    {
        plugs = this.transform.GetChild(0).GetComponentsInChildren<BU_Plug>();
    }

    // Update is called once per frame
    void Update()
    {

        totalEnergy = plugs[0].energy + plugs[1].energy + plugs[2].energy;

        if (buildingTypeAndBehaviour != null)
        {
            if (requiredEnergy > totalEnergy)
            {
                float time = 0;
                time += Time.deltaTime;
                if (time > 10)
                {
                    //DestroyBuilding();
                    Debug.Log("Destroyed");
                }
            }
        }
    }

    private void DestroyBuilding()
    {
        Destroy(buildingTypeAndBehaviour);
    }
}
