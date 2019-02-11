using UnityEngine;

public class Tutorial_ArmLock : Enemy
{
    [SerializeField] Tutorial_PlayerLock playerLock;
    [SerializeField] int legToRelease;
    [SerializeField] DissolveController dissolveController;

    public override void Die()
    {
        playerLock.LegRelease(legToRelease);
        enabled = false;
        dissolveController.StartDissolving();
    }

    public override void Update()
    {
        base.Update();
    }

    //Simple way to take damage
    public override void TakeDamage(int damage, Color damageType)
    {
        if (damageType == Color.green)
        {
            numberPool.NumberSpawn(numbersTransform, 0, damageType);
        }

        else
        {
            numberPool.NumberSpawn(numbersTransform, damage, damageType);

            StartCoroutine(gotHit());
            if (state == "Alive")
            {
                if (anim != null)
                {
                    anim.SetTrigger("Hit");
                }
                life -= damage;

                if (life <= 0)
                {
                    Die();
                    state = "Dead";
                }
            }
        }
    }

    //Max damage is decided by the laser, in case of future upgrades to the Laser. 
    public override void TakeWeakLaserDamage(float damage, int maxDamage)
    {
        TakeDamage(0, Color.green);
    }

}
