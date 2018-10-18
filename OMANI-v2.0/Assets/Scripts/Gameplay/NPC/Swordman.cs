using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : Robot
{
    [SerializeField] GameObject Attack1Zone;
    [SerializeField] GameObject Attack2Zone;
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
   
}
