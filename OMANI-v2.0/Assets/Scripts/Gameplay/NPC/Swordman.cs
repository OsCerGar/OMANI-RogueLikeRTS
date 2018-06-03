using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : NPC
{
    [SerializeField]ParticleSystem RageElectricity;
    void Awake()
    {
        boyType = "Swordsman";
    }
    public override void Update()
    {
        base.Update();
        checkVariables();
    }
    public void Explode()
    {
        Disenrage();
        transform.Find("Explosion").gameObject.SetActive(true);
        Die();
        anim.applyRootMotion = false;
    }
    public void AttackHit()
    {
       transform.Find("AttackZone").gameObject.SetActive(true);
    }
    public void Enrage()
    {
        RageElectricity.Play();
    }
    public void Disenrage()
    {
        RageElectricity.Stop();
    }
}
