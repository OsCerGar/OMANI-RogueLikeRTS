using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PowerManager : MonoBehaviour
{

    EZObjectPool BasicPower, UpgradedPower;
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
            if (item.PoolName == "BasicPower")
            {
                BasicPower = item;
            }
            if (item.PoolName == "UpgradedPower")
            {
                UpgradedPower = item;
            }
        }
    }
    private void Update()
    {
        if (PressUtoSpawn)
        {

            if (Input.GetKeyDown(KeyCode.U))
            {
                ShootBasicPower(Player);
            }
        }
    }

    public void ShootBasicPower(Transform tr)
    {
        BasicPower.TryGetNextObject(tr.position, tr.rotation, out Spawned);
    }
    public void ShootUpgradedPower(Transform tr)
    {
        UpgradedPower.TryGetNextObject(tr.position, tr.rotation, out Spawned);
    }
}

