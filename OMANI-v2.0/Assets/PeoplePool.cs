using EZObjectPools;
using UnityEngine;

public class PeoplePool : MonoBehaviour
{

    EZObjectPool Worker, Warrior, Shooter;
    GameObject Spawned;

    public static PeoplePool peoplePool;

    // Use this for initialization
    void Awake()
    {
        if (peoplePool == null) { peoplePool = this; }
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "Worker")
            {
                Worker = item;
            }
            if (item.PoolName == "Warrior")
            {
                Warrior = item;
            }
            if (item.PoolName == "Shooter")
            {
                Shooter = item;
            }
        }
    }

    public GameObject Spawn(Transform tr, Vector3 ps, string _name)
    {
        if (_name == "Worker" || _name == "worker")
        {
            return WorkerSpawn(tr, ps);
        }
        if (_name == "Warrior" || _name == "warrior")
        {
            return WarriorSpawn(tr);
        }
        if (_name == "Shooter" || _name == "shooter")
        {
            return ShooterSpawn(tr);
        }
        return null;
    }

    public GameObject WorkerSpawn(Transform tr, Vector3 ps)
    {
        Worker.TryGetNextObject(ps, tr.rotation, out Spawned);
        return Spawned;
    }

    public GameObject WarriorSpawn(Transform tr)
    {
        Warrior.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        return Spawned;

    }
    public GameObject ShooterSpawn(Transform tr)
    {
        Shooter.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        return Spawned;

    }
}
