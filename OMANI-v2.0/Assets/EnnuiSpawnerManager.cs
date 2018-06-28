using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class EnnuiSpawnerManager : MonoBehaviour
{

    EZObjectPool EnnuiParabola, Ennui, SwordsmanPool, ArcherPool;
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
            if (item.PoolName == "EnnuiParabola")
            {
                EnnuiParabola = item;
            }
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
        EnnuiParabola.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        Spawned.GetComponent<Animator>().SetBool("KnockBack", true);
    }
    public void SpawnEnnui(Transform tr)
    {
        Ennui.TryGetNextObject(tr.position, tr.rotation, out Spawned);
    }
}
