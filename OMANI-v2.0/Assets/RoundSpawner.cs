using System.Collections;
using UnityEngine;

public class RoundSpawner : MonoBehaviour
{
    EnemyPooler EPool;
    Transform Player;
    [SerializeField] string NameOfEnemyToSpawn;
    [SerializeField] Transform posToSpawn;
    GameObject thisEnemy;
    Enemy thisEnemyScript;
    [SerializeField] int numberToSpawn;
    int spawnNumber;

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
        if (spawnNumber < numberToSpawn)
        {
            if (enem == thisEnemyScript)
            {
                spawnNumber++;
                StartCoroutine("SpawnEnemyAfterTime");
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnEnemyAfterTime()
    {
        yield return new WaitForSeconds(10f);
        thisEnemy = EPool.SpawnEnemy(NameOfEnemyToSpawn, posToSpawn);
        thisEnemyScript = thisEnemy.GetComponent<Enemy>();
    }
}
