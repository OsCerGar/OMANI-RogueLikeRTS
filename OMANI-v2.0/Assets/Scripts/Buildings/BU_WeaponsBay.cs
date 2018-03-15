using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_WeaponsBay : MonoBehaviour {

    Component buildingTypeAndBehaviour;

    
    public int totalEnergy;
    [SerializeField]
    BU_Plug[] plugs;

	// Use this for initialization
	void Start () {
        plugs = this.transform.GetChild(0).GetComponentsInChildren<BU_Plug>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        totalEnergy = plugs[0].energy + plugs[1].energy + plugs[2].energy;
    }

}
