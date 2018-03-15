using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Swordsmith : MonoBehaviour
{

    BU_Equipment buildingEquipmentStore;
    GameObject equipmentToSpawn;

    // Use this for initialization
    void Start()
    {
        buildingEquipmentStore = this.transform.GetComponentInChildren<BU_Equipment>();
        StartCoroutine(CreateEquipment());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CreateEquipment()
    {

        do
        {
            buildingEquipmentStore.addEquipment(equipmentToSpawn);
            yield return new WaitForSeconds(30f);
        } while (true);

    }
}
