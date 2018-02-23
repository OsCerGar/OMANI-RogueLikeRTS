using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageCamp : MonoBehaviour {
    

    List <GameObject> nearbyResources;
    public GameObject[] Shacks;

    [HideInInspector]
    public int currentNumberOfShacks = 0;

    [HideInInspector]
    public int currentNumberOfShepHerd = 0;

    [SerializeField]
    int AreaOfResources;
    // Use this for initialization
    void Start () {

        selectPosibleResources(FindObjectOfType<MapManager>().ResourcePrefab) ;
        spawnCreeps();

    }

    private void spawnCreeps()
    {
        Debug.Log("TODO: Spawn Creeps");
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void createSavageShack()
    {
        bool completed = false;
        while (!completed)
        {
            var CampSelection = UnityEngine.Random.Range(0, Shacks.Length);
            Debug.Log(CampSelection);
            if (!Shacks[CampSelection].active)
            {
                Shacks[CampSelection].SetActive(true);
                completed = true;
                currentNumberOfShacks++;
            }
        }
    }
    GameObject[] selectPosibleResources(GameObject[] allRes)
    {
        for (int i = 0; i < allRes.Length; i++)
        {
            if (Vector3.Distance(transform.position,allRes[i].transform.position) < AreaOfResources)
            {
                nearbyResources.Add(allRes[i]);
            }
        }
        return allRes;
    }
    
}
