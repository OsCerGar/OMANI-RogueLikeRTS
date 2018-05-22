using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
public class PTR_ArenaSpawner : MonoBehaviour {

    EZObjectPool ChomperPool,GrenadierPool,SwordsmanPool,ArcherPool;
   public  Transform chomperSpawn, grenadierSpawn, swordsmanSpawn, archerSpawn;
    // Use this for initialization
    GameObject Spawned;
    void Start () {
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SwordsmanPool.TryGetNextObject(swordsmanSpawn.position, swordsmanSpawn.rotation, out Spawned);
            var spawnNpc = Spawned.GetComponent<NPC>();
            if (spawnNpc.life < spawnNpc.startLife)
            {
                spawnNpc.life = spawnNpc.startLife;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ArcherPool.TryGetNextObject(archerSpawn.position, archerSpawn.rotation, out Spawned);
            var spawnNpc = Spawned.GetComponent<NPC>();
            if (spawnNpc.life < spawnNpc.startLife)
            {
                spawnNpc.life = spawnNpc.startLife;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChomperPool.TryGetNextObject(chomperSpawn.position, chomperSpawn.rotation, out Spawned);
            var spawnNpc = Spawned.GetComponent<NPC>();
            if (spawnNpc.life < spawnNpc.startLife)
            {
                spawnNpc.life = spawnNpc.startLife;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GrenadierPool.TryGetNextObject(grenadierSpawn.position, grenadierSpawn.rotation, out Spawned);
            var spawnNpc = Spawned.GetComponent<NPC>();
            if (spawnNpc.life < spawnNpc.startLife)
            {
                spawnNpc.life = spawnNpc.startLife;
            }
        }
    }

}
