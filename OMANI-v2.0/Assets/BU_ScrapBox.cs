using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_ScrapBox : MonoBehaviour
{
    Transform equipment;
    AudioSource spitsound;

    [SerializeField]
    GameObject[] equipments;

    private void Start()
    {
        spitsound = this.GetComponent<AudioSource>();

        equipment = this.transform.Find("Equipment");
    }

    public void SpitEquipment()
    {
        GameObject Equipment = Instantiate(equipments[Random.Range(0, 1)], equipment.transform.position, equipment.transform.rotation);
        Equipment.GetComponent<Rigidbody>().AddForce(-Equipment.transform.forward * 6, ForceMode.Impulse);
        spitsound.Play();

    }
}
