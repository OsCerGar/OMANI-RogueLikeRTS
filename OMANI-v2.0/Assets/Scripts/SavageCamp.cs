﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageCamp : MonoBehaviour {
    
    private List<GameObject> Buildings = new List<GameObject> ()  ;
    
    

    [HideInInspector]
    public List<GameObject> currentBaddies = new List<GameObject>();
    

    [HideInInspector]
    public  bool someoneSearching = false;

    private bool alarm;

    private GameObject protagonist;

    private float buildCounter = 0, buildCooldown = 360;
    private float attackCounter = 0,attackCooldown = 300;
    // Use this for initialization
    void Start () {
        foreach (Transform child in transform)
        {
            Buildings.Add(child.gameObject);
        }

        protagonist = FindObjectOfType<Player>().gameObject;
        Evolve();
    }
    

    private void Evolve()
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            if (Buildings[i].GetComponent<Turret>() != null)
            {
                Buildings[i].SetActive(true);
            }
            else
            {
                if (Buildings[i].GetComponent<SavageShack>().enabled == false)
                {
                    Buildings[i].GetComponent<SavageShack>().enabled = true;
                    return;
                }
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

        if (attackCounter < attackCooldown)
        {
            attackCounter += Time.deltaTime;
        }
        else
        {
            AttackBase();
            attackCounter = 0;
        }

    }

    private void AttackBase()
    {
        foreach (GameObject baddie in currentBaddies)
        {
            //Attack protagonist
        }
        currentBaddies.Clear();
    }

    private void Alarm(GameObject intruder)
    {
        if (!alarm)
        {
            alarm = true;
            for (int i = 0; i < currentBaddies.Count; i++)
            {
                //intruder?
            }
        }
    }
    

}
