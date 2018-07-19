using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PeoplePool : MonoBehaviour
{

    EZObjectPool Worker, SpearWarrior;
    GameObject Spawned;

    // Use this for initialization
    void Start()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "Worker")
            {
                Worker = item;
            }
            if (item.PoolName == "SpearWarrior")
            {
                SpearWarrior = item;
            }
        }

    }

    public void WorkerSpawn(Transform tr)
    {
        Worker.TryGetNextObject(tr.position, tr.rotation, out Spawned);

    }
    public void SpearWarriorSpawn(Transform tr)
    {
        SpearWarrior.TryGetNextObject(tr.position, tr.rotation, out Spawned);
    }

}
