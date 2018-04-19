using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class WorldSpawnPos : WorldFeature
{
    EZObjectPool BasicCreepsPooler;
    private GameObject spawned;
    private Transform player;

    // Use this for initialization
    void Start () {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();

        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "BasicCreeps")
            {
                BasicCreepsPooler = item;
            }
        }
	}
    private void Update()
    {
        if (spawned != null)//If spawned a monster
        {
            if (Vector3.Distance(spawned.transform.position,player.position)>40)
            {
                spawned.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Player>() != null)
        {

            float randomizer = Random.Range(0, 100);
            
            if (randomizer < 20) //Posibilities to spawn something
            {
                randomizer = Random.Range(0, 100);

                if (randomizer < 5) //Posibilities to spawn something Special
                {
                    Debug.Log("something special");
                }
                else
                {
                    BasicCreepsPooler.TryGetNextObject(transform.position,transform.rotation,out spawned);
                    player = other.transform;
                }
            }
            else
            {
                Destroy(transform.gameObject);
            }
        }
        
    }
}
