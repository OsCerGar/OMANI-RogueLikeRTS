using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
public class EnemyPooler : MonoBehaviour {


    EZObjectPool ChomperPool, GrenadierPool, SwordsmanPool, ArcherPool;
    // Use this for initialization
    GameObject Spawned;
    void Start()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();

        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "Chomper")
            {
                ChomperPool = item;
            }
            if (item.PoolName == "Grenadier")
            {
                GrenadierPool = item;
            }
            if (item.PoolName == "Archer")
            {
                ArcherPool = item;
            }
            if (item.PoolName == "Swordsman")
            {
                SwordsmanPool = item;
            }
        }
    }
    public  GameObject SpawnEnemy(string _EnemyName, Transform spawnPos)
    {
        if (_EnemyName == "Chomper")
        {
            ChomperPool.TryGetNextObject(spawnPos.position, spawnPos.rotation, out Spawned);
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
