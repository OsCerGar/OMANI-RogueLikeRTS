using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPower : MonoBehaviour {
    [SerializeField] ParticleSystem mainParticleSystem;
    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
        
        if (mainParticleSystem.IsAlive() == false)
            transform.gameObject.SetActive(false);
            
    }
}
