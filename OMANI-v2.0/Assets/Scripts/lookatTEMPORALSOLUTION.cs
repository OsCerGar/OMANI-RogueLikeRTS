using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatTEMPORALSOLUTION : MonoBehaviour {

    public GameObject barroboy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(barroboy.transform);

	}

    private void OnTriggerStay(Collider other)
    {
        //En un futuro, R2/L2
        if (other.tag =="People" && Input.GetKey("joystick button 4"))
        {
            barroboy.GetComponent<Army>().Reclute(other.GetComponent<NPC>());
            Debug.Log("L1");
        }
    }
}
