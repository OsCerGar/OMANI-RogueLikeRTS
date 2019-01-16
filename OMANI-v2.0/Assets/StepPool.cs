using EZObjectPools;
using UnityEngine;

public class StepPool : MonoBehaviour
{
    EZObjectPool stepsand;
    GameObject Spawned;

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
        }

    }

    public void StepSpawn(Transform tr)
    {
        stepsand.TryGetNextObject(tr.position, stepsand.gameObject.transform.rotation, out Spawned);
    }

}
