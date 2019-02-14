public class Enemy : NPC
{

    public override void Update()
    {
        /*
        if (Nav != null)
        {
            TPC.Move(Nav.desiredVelocity);
        }
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
    public override void Die()
    {
        base.Die();
        enabled = false;
    }
}
