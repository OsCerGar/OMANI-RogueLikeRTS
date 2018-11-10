using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PowerManager : MonoBehaviour
{

    EZObjectPool BasicPower, UpgradedPower, Link, SelectionAnimation;
    GameObject Spawned;
    [SerializeField] bool PressUToBasic, PressIToUpgraded;
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
            if (item.PoolName == "Link")
            {
                Link = item;
            }
            if (item.PoolName == "SelectionAnimation")
            {
                SelectionAnimation = item;
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
    public GameObject CreateLink(Transform tr, Powers _powers)
    {
        Link.TryGetNextObject(Vector3.zero, Quaternion.identity, out Spawned);
        return Spawned;
    }
    public GameObject SpawnSelectionAnimation(Transform tr)
    {
        SelectionAnimation.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        return Spawned;
    }
}

