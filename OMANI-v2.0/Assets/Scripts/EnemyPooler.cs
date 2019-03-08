using EZObjectPools;
using UnityEngine;
public class EnemyPooler : MonoBehaviour
{


    EZObjectPool SurkaMele, SurkaRanged, CorruptedDemon;
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
            if (item.PoolName == "SurkaRanged")
            {
                SurkaRanged = item;
            }
            if (item.PoolName == "CorruptedDemon")
            {
                CorruptedDemon = item;
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
