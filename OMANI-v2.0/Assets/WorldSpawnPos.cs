using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class WorldSpawnPos : WorldFeature
{
    EZObjectPool BasicCreepsPooler;
    private GameObject spawned;
    private bool isSpawner = false;
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
           
            if (Vector3.Distance(spawned.transform.position,player.position)>100)
            {
                Debug.Log("deactivate");
                spawned.SetActive(false);
                spawned = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isSpawner)
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
                        Debug.Log("spawn");
                        if (BasicCreepsPooler != null)
                        {
                            BasicCreepsPooler.TryGetNextObject(transform.position, transform.rotation, out spawned);
                            player = other.transform;
                            isSpawner = true;
                        }
                        else
                        {
                            Destroy(transform.gameObject);
                        }
                    }
                }
                else
                {
                    Destroy(transform.gameObject);
                }
            }

        } else
        {
            if (!spawned.activeSelf)
            {
                spawned.SetActive(true);
            }
        }

    }
}
