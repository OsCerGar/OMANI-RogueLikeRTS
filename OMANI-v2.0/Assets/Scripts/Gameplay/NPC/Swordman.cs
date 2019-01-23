using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : Robot
{
    [SerializeField] GameObject Attack1Zone;
    [SerializeField] GameObject Attack2Zone;
    [SerializeField] int numberOfAttacks = 4;
    int attackcounter = 0;
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
    
    public override void FighterAttack(GameObject attackPosition)
    {
        if (attackcounter < numberOfAttacks)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("FinalAttack", true);
        }
    }
    public void AttackCounterPlusOne()
    {
        attackcounter++;
    }
    public override void CoolDown()
    {
        attackcounter = 0;
        reducePowerNow(maxpowerPool);
        enableTree("CoolDown");
        base.CoolDown();
    }
}
