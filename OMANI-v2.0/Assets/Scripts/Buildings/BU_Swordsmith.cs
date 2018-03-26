using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Swordsmith : MonoBehaviour
{

    BU_Equipment buildingEquipmentStore;
    BU_WeaponsBay weaponsBay;

    [SerializeField]
    GameObject equipmentToSpawn;

    [SerializeField]
    int itemsSpawned = 0;

    // Use this for initialization
    void Start()
    {
        buildingEquipmentStore = this.transform.GetComponentInChildren<BU_Equipment>();
        weaponsBay = this.transform.GetComponent<BU_WeaponsBay>();
        StartCoroutine(CreateEquipment());
    }

    IEnumerator CreateEquipment()
    {

        do
        {
            AddEquipment();
            yield return new WaitForSeconds(30f);
        } while (true);

    }


    private void AddEquipment()
    {
        buildingEquipmentStore.addEquipment(equipmentToSpawn);
        itemsSpawned++;

        switch (itemsSpawned)
        {
            case 1:
                weaponsBay.requiredEnergy += 1;
                break;
            case 5:
                weaponsBay.requiredEnergy += 1;
                break;
            case 9:
                weaponsBay.requiredEnergy += 1;
                break;
            case 12:
                weaponsBay.requiredEnergy += 1;
                break;
        }
    }
}
