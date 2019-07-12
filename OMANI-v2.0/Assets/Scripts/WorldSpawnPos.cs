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

    public delegate void Summoned();
    public static event Summoned OnSummon;

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
                    //OnSummon();
                }
                yield return new WaitForSeconds(1f);
            }
        }

    }
}
