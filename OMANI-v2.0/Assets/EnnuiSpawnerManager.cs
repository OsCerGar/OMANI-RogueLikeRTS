using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class EnnuiSpawnerManager : MonoBehaviour {

    EZObjectPool EnnuiParabola, EnnuiGround, SwordsmanPool, ArcherPool;
    GameObject Spawned;
    [SerializeField]bool PressUtoSpawn;
    Transform Player;
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player").transform;
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "EnnuiParabola")
            {
                EnnuiParabola = item;
            }
            if (item.PoolName == "EnnuiGround")
            {
                EnnuiGround = item;
            }
        }
    }
    private void Update()
    {
        if (PressUtoSpawn)
        {

            if (Input.GetKeyDown(KeyCode.U))
            {
                SpawnEnnui(Player);
            }
        }
    }

    public void SpawnEnnui(Transform tr)
    {
        EnnuiParabola.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        Spawned.GetComponent<Animator>().SetBool("KnockBack",true) ;
    }
}
