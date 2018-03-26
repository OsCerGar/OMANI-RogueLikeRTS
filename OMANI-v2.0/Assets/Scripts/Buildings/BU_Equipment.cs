using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Equipment : Interactible
{

    public ArrayList equipment = new ArrayList();
    Transform lastParent;

    public void addEquipment(GameObject _equipment)
    {
        equipment.Add(_equipment);
    }

    public override void Action(BoyMovement _boy)
    {

        if (equipment.Count > 0)
        {

            GameObject Equipment = Instantiate((GameObject)equipment[equipment.Count - 1]);
            equipment.Remove(equipment[equipment.Count - 1]);

            //Grabs
            _boy.grabbedObject = Equipment.GetComponent<Interactible>();
            lastParent = this.transform.parent;
            Equipment.transform.SetParent(_boy.hand.transform);
            Equipment.transform.localPosition = Vector3.zero;
        }

    }

    public void DestroyEquipment() {
        equipment.Clear();
    }
}

