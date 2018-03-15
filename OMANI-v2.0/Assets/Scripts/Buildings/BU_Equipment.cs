using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Equipment : MonoBehaviour {

    [SerializeField]
    ArrayList equipment = new ArrayList();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addEquipment(GameObject _equipment) {
        equipment.Add(_equipment);
    }
}
