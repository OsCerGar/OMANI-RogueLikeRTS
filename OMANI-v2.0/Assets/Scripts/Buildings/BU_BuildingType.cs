using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_BuildingType : MonoBehaviour
{

    BU_Equipment buildingEquipmentStore;
    BU_EquipmentBuildings weaponsBay;

    [SerializeField]
    public GameObject equipmentToSpawn;

    [SerializeField]
    int itemsSpawned = 0;
    [SerializeField]
    float timeToSpawn = 45, timeToSpawnCounter = 0;

    BU_WeaponsBay_GUI weaponsBayGUI;

    // Use this for initialization
    public virtual void Start()
    {
        buildingEquipmentStore = this.transform.GetComponentInChildren<BU_Equipment>();
        weaponsBay = this.transform.GetComponent<BU_EquipmentBuildings>();
        weaponsBay.buildingTypeAndBehaviour = this;

        StartCoroutine(CreateEquipment());

        weaponsBayGUI = this.transform.GetComponentInChildren<BU_WeaponsBay_GUI>();
    }

    public virtual void Update()
    {
        if (timeToSpawnCounter < timeToSpawn)
        {
            timeToSpawnCounter += Time.deltaTime;
        }
        weaponsBayGUI.ChangeEquipmentClock(SpawningTimes());

        switch (weaponsBay.numberOfWorkers)
        {
            case 0:
                timeToSpawn = 45;
                break;

            case 1:
                timeToSpawn = 35;
                break;

            case 2:
                timeToSpawn = 20;
                break;
        }

    }

    public virtual float SpawningTimes()
    {
        return timeToSpawnCounter / timeToSpawn;
    }

    public virtual float CreationTime()
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



    public virtual void AddEquipment()
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

    public virtual void OnDestroy()
    {
        weaponsBayGUI.ChangeEquipmentClock(0);

        buildingEquipmentStore.DestroyEquipment();
    }
}
