using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Equipment : MonoBehaviour {

    public ArrayList equipment = new ArrayList();

    public void addEquipment(GameObject _equipment) {
        equipment.Add(_equipment);
    }
}
