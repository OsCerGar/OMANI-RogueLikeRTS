using UnityEngine;
using System.Collections;

public class PowerDebugger : MonoBehaviour {

    public PowerManager PM;
    public Transform CannonMouth;

    private void Start()
    {
        PM = FindObjectOfType<PowerManager>();
    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKeyUp(KeyCode.P))
        {
            PM.ShootBasicPower(CannonMouth);
        }
        
	
	}
}
