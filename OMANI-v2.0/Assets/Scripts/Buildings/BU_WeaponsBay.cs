using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_WeaponsBay : MonoBehaviour
{

    [SerializeField]
    Component buildingTypeAndBehaviour;


    public int totalEnergy, requiredEnergy;
    float time = 0;

    [SerializeField]
    BU_Plug[] plugs;

    MeshRenderer[] plugMaterial = new MeshRenderer[3];


    // Use this for initialization
    void Start()
    {
        plugs = this.transform.GetChild(0).GetComponentsInChildren<BU_Plug>();
        foreach (Component comp in transform.GetComponents<Component>())
        {
            if (!comp.name.Equals("BU_WeaponsBay"))
            {
                buildingTypeAndBehaviour = comp;
            }
        }

        int i = 0;
        foreach (BU_Plug plug in plugs)
        {
            plugMaterial[i] = plug.gameObject.GetComponent<MeshRenderer>();
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

        totalEnergy = plugs[0].energy + plugs[1].energy + plugs[2].energy;

        if (buildingTypeAndBehaviour != null)
        {
            if (requiredEnergy > totalEnergy)
            {
                if (requiredEnergy < 4)
                {

                    switch (requiredEnergy)
                    {
                        case 1:
                            plugMaterial[0].material.color = Color.red;
                            break;
                        case 2:
                            plugMaterial[1].material.color = Color.red;
                            break;
                        case 3:
                            plugMaterial[2].material.color = Color.red;
                            break;
                    }
                }

                time += Time.deltaTime;

                if (time > 10)
                {
                    //DestroyBuilding();
                    Debug.Log("Destroyed");
                    time = 0;
                }
            }
            else
            {
                time = 0;
            }
        }
    }

    private void DestroyBuilding()
    {
        Destroy(buildingTypeAndBehaviour);
    }
}
