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
    
    void OnEnable()
    {

        Player = FindObjectOfType<Player>().transform;
        EPool = FindObjectOfType<EnemyPooler>();
        Enemy.OnDie += SpawnEnemy;
        thisEnemy = EPool.SpawnEnemy(NameOfEnemyToSpawn, posToSpawn);
        thisEnemyScript = thisEnemy.GetComponent<Enemy>();
    }


    void OnDisable()
    {
        Enemy.OnDie -= SpawnEnemy;
    }

    private void SpawnEnemy(Enemy enem)
    {
        if (enem = thisEnemyScript)
        {
            thisEnemy = EPool.SpawnEnemy(NameOfEnemyToSpawn, posToSpawn);
            thisEnemyScript = thisEnemy.GetComponent<Enemy>();
        }
    }
}
