using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : Robot
{
    [SerializeField] GameObject Attack1Zone;
    [SerializeField] GameObject Attack2Zone;
    [SerializeField] GameObject ContinuousAttackZone;
    void Awake()
    {
        boyType = "Swordsman";
    }
    public override void Update()
    {
        base.Update();
    }
    public override void AttackHit()
    {
        base.AttackHit();
    }
    public void Attack1()
    {
        Attack1Zone.SetActive(true);
    }
    public void Attack2()
    {
        Attack2Zone.SetActive(true);
    }
    public void ContinuousAttack()
    {
        ContinuousAttackZone.SetActive(true);
    }
    public override void FighterAttack(GameObject attackPosition)
    {
        anim.SetTrigger("Attack");
    }
}
