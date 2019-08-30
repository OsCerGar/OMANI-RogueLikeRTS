using System.Collections;
using UnityEngine;

public class DeathRoomSpawner : MonoBehaviour
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
        if (thisEnemy != null)
        {
            thisEnemyScript = thisEnemy.GetComponent<Enemy>();
        }

    }


    void OnDisable()
    {
        Enemy.OnDie -= SpawnEnemy;
    }


    private void SpawnEnemy(Enemy enem)
    {
        if (enem == thisEnemyScript)
        {
            StartCoroutine("SpawnEnemyAfterTime");
        }
    }

    private IEnumerator SpawnEnemyAfterTime()
    {
        yield return new WaitForSeconds(2f);
        thisEnemy = EPool.SpawnEnemy(NameOfEnemyToSpawn, posToSpawn);
        if (thisEnemy != null)
        {
            thisEnemyScript = thisEnemy.GetComponent<Enemy>();
        }
    }
}
