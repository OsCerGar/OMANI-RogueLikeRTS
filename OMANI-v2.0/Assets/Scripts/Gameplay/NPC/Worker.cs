using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Robot {
    public GameObject Scrap;
    [SerializeField]
    private ParticleSystem TrailEffect;

    [SerializeField]
    private GameObject RollHillBox;

    void Awake()
    {
        boyType = "Worker";
    }
    public void Trail()
    {
        TrailEffect.Play();
    }
    public void FlipSound()
    {
        WorkerSM wsm = (WorkerSM)SM;
        wsm.Flip();
    }
    public void RollAttack()
    {
        RollHillBox.SetActive(true);
    }
}
