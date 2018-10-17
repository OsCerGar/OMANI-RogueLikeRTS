using EZObjectPools;
using UnityEngine;
public class EnemyPooler : MonoBehaviour
{


    EZObjectPool SurkaMele, SurkaRat, SwordsmanPool, ArcherPool;
    // Use this for initialization
    GameObject Spawned;
    void Start()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();

        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "SurkaMele")
            {
                SurkaMele = item;
            }
            if (item.PoolName == "SurkaRat")
            {
                SurkaRat = item;
            }
        }
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
        else if (_EnemyName == "SurkaRat")
        {
            SurkaRat.TryGetNextObject(spawnPos.position, spawnPos.rotation, out Spawned);
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
