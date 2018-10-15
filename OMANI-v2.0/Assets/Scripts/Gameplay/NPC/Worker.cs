using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Robot {
    public GameObject Scrap;
    [SerializeField]
    private ParticleSystem TrailEffect, SparkEffect;

    [SerializeField]
    private GameObject RollHillBox;

    public override void AttackHit()
    {
        base.AttackHit();
        RollAttackFinished();
    }
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
    public void RollAttackFinished()
    {
        RollHillBox.SetActive(false);
    }
    public void RollCollision()
    {
        anim.SetTrigger("AttackCollision");
    }

    public void Sparks()
    {
        SparkEffect.Play();
    }
}
