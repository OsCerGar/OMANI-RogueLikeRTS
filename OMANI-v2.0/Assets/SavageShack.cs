using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using System;

public class SavageShack : MonoBehaviour {
    [SerializeField]
    float cooldown = 30f;
    float cooldownCounter = 0f;
    SavageCamp camp;
    GameObject altar;
    public GameObject ShepherdPref;
    Transform SpawnPoint;

    // Use this for initialization
    void Start () {
        camp = transform.parent.GetComponent<SavageCamp>();
        altar = transform.parent.transform.Find("Altar").gameObject;
        SpawnPoint = transform.Find("SpawnPoint");
	}
	
	// Update is called once per frame
	void Update () {
		
            //If the Population is not full, create a Shepherd to the colony
            if (camp.currentShepHerds.Count < camp.currentNumberOfShacks)
            {
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
                //SetAltar Variable
                var stateVariable = (SharedGameObject)shepH.GetComponent<BehaviorTree>().GetVariable("Altar");
                stateVariable.Value = transform.parent.transform.Find("Altar").gameObject;
                //Add to the list of the camp :D
                camp.currentShepHerds.Add(shepH);
                
                }
            }
        
	}
}
