using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using System;

public class SavageShack : MonoBehaviour {
    [SerializeField]
    float cooldown = 90f;
    float cooldownCounter = 0f;
    SavageCamp camp;
    GameObject altar;
    public GameObject ShepherdPref;
    Transform SpawnPoint;

    // Use this for initialization
    void Start () {
        camp = transform.parent.GetComponent<SavageCamp>();
        SpawnPoint = transform.Find("SpawnPoint");
	}
	
	// Update is called once per frame
	void Update () {
		
            //If the Population is not full, create a Shepherd to the colony
            
                if (cooldownCounter < cooldown)
                {
                    cooldownCounter += Time.deltaTime;
                    // TODO: Represent Graphicaly
                }
                else
                {
                    //instanciate Shepherd
                    Debug.Log("Shepherd!");
                    var shepH = Instantiate(ShepherdPref,SpawnPoint.position, SpawnPoint.rotation);
                     cooldownCounter = 0;
                    camp.currentBaddies.Add(shepH);


                }
            
        
	}
}
