using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Archer : MonoBehaviour
{


    BU_Equipment buildingEquipmentStore;
    BU_WeaponsBay weaponsBay;

    [SerializeField]
    public GameObject equipmentToSpawn;

    [SerializeField]
    int itemsSpawned = 0;
    float timeToSpawn = 45, timeToSpawnCounter = 0;

    BU_WeaponsBay_GUI weaponsBayGUI;

    // Use this for initialization
    void Start()
    {
        buildingEquipmentStore = this.transform.GetComponentInChildren<BU_Equipment>();
        weaponsBay = this.transform.GetComponent<BU_WeaponsBay>();
        weaponsBay.buildingTypeAndBehaviour = this;

        StartCoroutine(CreateEquipment());

        weaponsBayGUI = this.transform.GetComponentInChildren<BU_WeaponsBay_GUI>();
    }

    private void Update()
    {
        if (timeToSpawnCounter < timeToSpawn)
        {
            timeToSpawnCounter += Time.deltaTime;
        }
        weaponsBayGUI.ChangeEquipmentClock(SpawningTimes());
    }

    public float SpawningTimes()
    {
        return timeToSpawnCounter / timeToSpawn;
    }

    public float CreationTime()
    {
        return (float)itemsSpawned / 7;
    }


    IEnumerator CreateEquipment()
    {

        do
        {
            AddEquipment();
            timeToSpawnCounter = 0;
            yield return new WaitForSeconds(timeToSpawn);
        } while (true);

    }



    private void AddEquipment()
    {
        buildingEquipmentStore.addEquipment(equipmentToSpawn);
        itemsSpawned++;

        weaponsBay.ReturnCreationTime(CreationTime());

        switch (itemsSpawned)
        {
            case 2:
                weaponsBay.requiredEnergy += 1;
                //If the required energy is bigger than what the building has, it will turn a plug red.
                weaponsBay.TurnToRed();

                break;
            case 4:
                weaponsBay.requiredEnergy += 1;
                //If the required energy is bigger than what the building has, it will turn a plug red.
                weaponsBay.TurnToRed();

                break;
            case 6:
                weaponsBay.requiredEnergy += 1;
                //If the required energy is bigger than what the building has, it will turn a plug red.
                weaponsBay.TurnToRed();

                break;
            case 8:
                weaponsBay.requiredEnergy += 1;
                //If the required energy is bigger than what the building has, it will turn a plug red.
                weaponsBay.TurnToRed();
                break;
        }

    }

    private void OnDestroy()
    {
        weaponsBayGUI.ChangeEquipmentClock(0);

        buildingEquipmentStore.DestroyEquipment();
    }
}
