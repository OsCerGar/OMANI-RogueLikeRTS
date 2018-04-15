using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Equipment : Interactible
{

    public ArrayList equipment = new ArrayList();
    Transform lastParent;
    AudioSource spitsound;

    public override void Start()
    {
        spitsound = this.GetComponent<AudioSource>();
    }

    public void addEquipment(GameObject _equipment)
    {
        equipment.Add(_equipment);

        SpitEquipment();
    }

    public override void Action(BoyMovement _boy)
    {


    }

    public void DestroyEquipment()
    {
        equipment.Clear();
    }

    public void SpitEquipment()
    {
        
        GameObject Equipment = Instantiate((GameObject)equipment[equipment.Count - 1], this.transform.position, this.transform.rotation);
        Equipment.GetComponent<Rigidbody>().AddForce(Equipment.transform.forward * 6, ForceMode.Impulse);
        spitsound.Play();

        equipment.Remove(equipment[equipment.Count - 1]);   
    }

}

