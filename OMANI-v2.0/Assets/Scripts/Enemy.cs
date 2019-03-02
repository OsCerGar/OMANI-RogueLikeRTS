
using UnityEngine;

public class Enemy : NPC
{

    public override void Update()
    {
        
        if (Nav != null)
        {
            TPC.Move(Nav.desiredVelocity);
        }
        /*
        if (anim != null)
        {
            if (!RootMotion)
            {
                anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);
            }
        }
        */
    }
    public override void AttackHit()
    {
        Attackzone.SetActive(true);
    }

    private void OnEnable()
    {
        
    }
    public override void Die()
    {
        base.Die();
    }
}
