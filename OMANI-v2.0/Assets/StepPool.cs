using EZObjectPools;
using UnityEngine;

public class StepPool : MonoBehaviour
{
    EZObjectPool stepsand, stepsandrear;
    GameObject Spawned;
    public static StepPool stepPool;

    private void Awake()
    {
        if (stepPool == null)
        {
            stepPool = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "StepSand")
            {
                stepsand = item;
            }
            if (item.PoolName == "StepSandRear")
            {
                stepsandrear = item;
            }
        }

    }

    public void StepSpawn(Transform tr)
    {
        stepsand.TryGetNextObject(new Vector3(tr.position.x, tr.position.y - 1f, tr.position.z), stepsand.gameObject.transform.rotation, out Spawned);
    }
    public void StepSpawnRear(Transform tr)
    {
        stepsandrear.TryGetNextObject(new Vector3(tr.position.x, tr.position.y - 0.4f, tr.position.z), stepsandrear.gameObject.transform.rotation, out Spawned);
    }
}
