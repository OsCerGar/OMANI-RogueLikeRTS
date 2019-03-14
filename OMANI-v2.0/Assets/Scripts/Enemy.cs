
using UnityEngine;

public class Enemy : NPC
{
    public Transform laserTarget;
    private void Awake()
    {
        laserTarget = transform.FindDeepChild("LaserObjective");
        laserTarget.gameObject.SetActive(true);

    }
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
        if (laserTarget != null)
        {
            laserTarget.gameObject.SetActive(true);
        }
    }
    public override void Die()
    {
        base.Die();
        laserTarget.gameObject.SetActive(false);

    }

    public void StepSound()
    {
        SM.Step();
    }
}
