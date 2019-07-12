using EZObjectPools;
using System.Collections.Generic;
using UnityEngine;
public class EnemyPooler : MonoBehaviour
{

    public static EnemyPooler enemypool;
    EZObjectPool SurkaMele, SurkaRanged, CorruptedDemon;

    // Use this for initialization
    GameObject Spawned;

    [SerializeField]
    List<string> enemiesAvailable = new List<string>();
    void Start()
    {
        if (enemypool == null) { enemypool = this; }
        var AllPoolers = FindObjectsOfType<EZObjectPool>();

        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "SurkaMele")
            {
                SurkaMele = item;
                enemiesAvailable.Add("SurkaMele");
            }
            if (item.PoolName == "SurkaRanged")
            {
                SurkaRanged = item;
                enemiesAvailable.Add("SurkaRanged");

            }
            if (item.PoolName == "CorruptedDemon")
            {
                CorruptedDemon = item;
                enemiesAvailable.Add("CorruptedDemon");

            }
        }
    }

    public GameObject RandomSpawnEnemies(int points, Transform spawnPos)
    {
        if (points >= 25)
        {
            int randomEnemy = Random.Range(0, enemiesAvailable.Count);
            if (points >= int.Parse(GamemasterController.GameMaster.getCsvValues(enemiesAvailable[randomEnemy])[3]))
            {
                points -= int.Parse(GamemasterController.GameMaster.getCsvValues(enemiesAvailable[randomEnemy])[3]);
                SpawnEnemy(GamemasterController.GameMaster.getCsvValues(enemiesAvailable[randomEnemy])[0], spawnPos);

            }
        }

        else { RandomSpawnEnemies(points, spawnPos); }
        return Spawned;
    }

    public GameObject SpawnEnemy(string _EnemyName, Transform spawnPos)
    {
        if (_EnemyName == "SurkaMele")
        {
            SurkaMele.TryGetNextObject(spawnPos.position, spawnPos.rotation, out Spawned);
            if (Spawned != null)
            {
                var spawnNpc = Spawned.GetComponent<NPC>();
                if (spawnNpc.life < spawnNpc.startLife)
                {
                    spawnNpc.life = spawnNpc.startLife;
                }
            }

        }
        else if (_EnemyName == "SurkaRanged")
        {
            SurkaRanged.TryGetNextObject(spawnPos.position, spawnPos.rotation, out Spawned);
            if (Spawned != null)
            {
                var spawnNpc = Spawned.GetComponent<NPC>();
                if (spawnNpc.life < spawnNpc.startLife)
                {
                    spawnNpc.life = spawnNpc.startLife;
                }
            }

        }
        else if (_EnemyName == "CorruptedDemon")
        {
            CorruptedDemon.TryGetNextObject(spawnPos.position, spawnPos.rotation, out Spawned);
            if (Spawned != null)
            {
                var spawnNpc = Spawned.GetComponent<NPC>();
                if (spawnNpc.life < spawnNpc.startLife)
                {
                    spawnNpc.life = spawnNpc.startLife;
                }
            }

        }

        return Spawned;
    }
}
