using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    EnemyPooler EPool;
    Transform Player;
    [SerializeField] string NameOfEnemyToSpawn;
    [SerializeField] Transform posToSpawn;
    GameObject thisEnemy;
    Enemy thisEnemyScript;
    bool spawn = false;
    
    void OnEnable()
    {

        Player = FindObjectOfType<Player>().transform;
        EPool = FindObjectOfType<EnemyPooler>();
        Enemy.OnDie += SpawnEnemy;
        thisEnemy = EPool.SpawnEnemy(NameOfEnemyToSpawn, posToSpawn);
        thisEnemyScript = thisEnemy.GetComponent<Enemy>();
        spawn = true;
    }


    void OnDisable()
    {
        Enemy.OnDie -= SpawnEnemy;
    }

    private void SpawnEnemy(Enemy enem)
    {
        if (spawn)
        {
            if (enem == thisEnemyScript)
            {
                spawn = false;

                StartCoroutine("SpawnEnemyAfterTime");
            }
        }
    }

    private IEnumerator SpawnEnemyAfterTime()
    {

        yield return new WaitForSeconds(5f);
        spawn = true;
        thisEnemy = EPool.SpawnEnemy(NameOfEnemyToSpawn, posToSpawn);
            thisEnemyScript = thisEnemy.GetComponent<Enemy>();
            spawn = false;
    }
}
