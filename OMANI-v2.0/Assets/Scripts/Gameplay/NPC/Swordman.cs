using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : NPC
{
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
        transform.Find("Explosion").gameObject.SetActive(true);
        Die();
        anim.applyRootMotion = false;
    }
    public void AttackHit()
    {
       transform.Find("AttackZone").gameObject.SetActive(true);
    }
}
