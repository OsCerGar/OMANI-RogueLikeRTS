using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageShack : MonoBehaviour {
    
    float cooldown = 30f;
    float cooldownCounter = 0f;
    SavageCamp camp;

    // Use this for initialization
    void Start () {
        camp = transform.parent.GetComponent<SavageCamp>();
	}
	
	// Update is called once per frame
	void Update () {
		
            if (camp.currentNumberOfShepHerd < camp.currentNumberOfShacks)
            {
                if (cooldownCounter < cooldown)
                {
                    cooldownCounter += Time.deltaTime;
                    // TODO: Represent Graphicaly
                }else
                {
                //instanciate Shepherd
                Debug.Log("Shepherd!");
                camp.currentNumberOfShepHerd++;

                }
            }
        
	}
}
