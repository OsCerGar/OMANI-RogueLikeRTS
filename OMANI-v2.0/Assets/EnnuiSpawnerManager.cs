using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.UI;

public class EnnuiSpawnerManager : MonoBehaviour
{

    EZObjectPool Ennui, SwordsmanPool, ArcherPool;
    GameObject Spawned;
    [SerializeField] bool PressUtoSpawn;
    Transform Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "Ennui")
            {
                Ennui = item;
            }
        }

    }
    private void Update()
    {
        if (PressUtoSpawn)
        {

            if (Input.GetKeyDown(KeyCode.U))
            {
                SpawnEnnuiParabola(Player);
            }
        }
    }

    public void SpawnEnnuiParabola(Transform tr)
    {
        Ennui.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        Spawned.GetComponent<Rigidbody>().AddForce(transform.forward * 2, ForceMode.Impulse);
    }
    public void SpawnEnnui(Transform tr)
    {
        Ennui.TryGetNextObject(tr.position, tr.rotation, out Spawned);
    }
}
