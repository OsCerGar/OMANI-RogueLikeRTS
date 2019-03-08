using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class WorldSpawnPos : MonoBehaviour
{
    bool FoundPlayer = false;
    EnemyPooler EPool;
    Transform Player;
    [SerializeField] string NameOfEnemyToSpawn;
    private void Start()
    {
        Player = FindObjectOfType<Player>().transform;
        EPool = FindObjectOfType<EnemyPooler>();

        StartCoroutine(CheckDistance());
    }

    IEnumerator CheckDistance()
    {
        if (Player!= null)
        {

            while(!FoundPlayer)
            {
                if (Vector3.Distance(transform.position, Player.position) < 30)
                {
                    EPool.SpawnEnemy(NameOfEnemyToSpawn, transform);
                    FoundPlayer = true;
                }
                else {
                    Debug.Log("too far" + Vector3.Distance(transform.position, Player.position));
                        }

                yield return new WaitForSeconds(1f);
            }
        }

    }
}
