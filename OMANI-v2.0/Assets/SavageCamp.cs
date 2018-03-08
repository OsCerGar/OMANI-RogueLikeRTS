using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageCamp : MonoBehaviour {

    MapManager mManager;
    public GameObject[] Shacks;

    [HideInInspector]
    public int currentNumberOfShacks = 0;
    

    [HideInInspector]
    public List<GameObject> currentShepHerds = new List<GameObject>();

    [SerializeField]
    int AreaOfResources;

    [HideInInspector]
    public  bool someoneSearching = false;
    // Use this for initialization
    void Start () {

        mManager = transform.parent.GetComponent<MapManager>();
        
    }
    

    // Update is called once per frame
    void Update () {
		if (!someoneSearching)
        {
            if (currentShepHerds.Count > 0)
            {
                
                //Here we make him go search for resources!!!
                var resToGo = GetRandomRes();
                if (resToGo != null)
                {
                    currentShepHerds[UnityEngine.Random.Range(0, currentShepHerds.Count-1)].GetComponent<ShepHerd>().SetTarget(resToGo);
                    someoneSearching = true;

                }
            }
            
        }
	}

    //Get one of the non Active Shacks, and makes it Active.
    public void createSavageShack()
    {
        for (int i = 0; i < Shacks.Length; i++)
        {
            if (!Shacks[i].activeSelf)
            {
                Shacks[i].SetActive(true);
                currentNumberOfShacks++;
                return;
            }
        }
        
    }
    

    public GameObject GetRandomRes()
    {
       
       if (mManager.Res.Count > 0)
            {

                for (int i = 0; i < mManager.Res.Count; i++)
                {
                    if (mManager.Res[i].activeSelf)
                    {
                    
                        return mManager.Res[i];
                    }
                }
                
        }else
        {
            Debug.Log("NoRess");
        }   
        return null;
    }
    
}
