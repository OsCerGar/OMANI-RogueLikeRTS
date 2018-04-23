using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageCamp : MonoBehaviour {
    
    public GameObject[] Buildings;

    [HideInInspector]
    public int currentNumberOfShacks = 0;
    

    [HideInInspector]
    public List<GameObject> currentShepHerds = new List<GameObject>();

    [SerializeField]
    int AreaOfResources;

    [HideInInspector]
    public  bool someoneSearching = false;

    private bool alarm;

    private float buildCounter = 0, buildCooldown = 240;
    private float alarmCounter = 0,alarmCooldown = 20;
    // Use this for initialization
    void Start () {
             
        Evolve();
        
    }

    private void Evolve()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            if (!Buildings[i].activeSelf)
            {
                Buildings[i].SetActive(true);
                currentNumberOfShacks++;
                return;
            }
        }
    }

    
    void Update () {
        //Evolving every buildCooldown (adds a building)
		if (buildCounter<buildCooldown)
        {
            buildCounter += Time.deltaTime;
        }else
        {
            Evolve();
            buildCounter = 0;
        }

        if (alarm)
        {
            
           //Defend Base

        }
        
	}

 
    private void Alarm(GameObject intruder)
    {
        if (!alarm)
        {
            alarm = true;
            for (int i = 0; i < currentShepHerds.Count; i++)
            {
                currentShepHerds[i].GetComponent<ShepHerd>().SetTarget(intruder);
            }
        }
    }
    

  
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy") 
        {
            if (other.GetComponent<ShepHerd>() != null) //If a Shepherd enters, then he's home
            {
                other.GetComponent<ShepHerd>().SetHome(true);
            }
        }
        if (other.tag == "People") //If people enters, set alarm!!
        {
            Alarm(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<ShepHerd>() != null)
            {
                other.GetComponent<ShepHerd>().SetHome(false);
            }
        }
    }

}
