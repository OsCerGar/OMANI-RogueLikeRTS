using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.UI;

public class EnnuiSpawnerManager : MonoBehaviour
{

    EZObjectPool Ennui;
    GameObject Spawned;

    // Use this for initialization
    void Start()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "EnnuiSphere")
            {
                Ennui = item;
            }
        }

    }

    public void SpawnEnnuiParabola(Transform tr)
    {
        Ennui.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        Spawned.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(2,3), ForceMode.Impulse);
    }
    public void SpawnEnnui(Transform tr)
    {
        Debug.Log("Spawned");
        Ennui.TryGetNextObject(tr.position, Quaternion.identity, out Spawned);
    }
}
