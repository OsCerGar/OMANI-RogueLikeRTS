﻿using UnityEngine;

public class Tutorial_ArmLock : Enemy
{
    [SerializeField] Tutorial_PlayerLock playerLock;
    [SerializeField] int legToRelease;
    [SerializeField] DissolveController dissolveController;

    public override void Die()
    {
        playerLock.LegRelease(legToRelease);
        //Renderer renderer = GetComponent<Renderer>();
        //MK.Toon.MKToonMaterialHelper.SetRimIntensity(renderer.material, 0f);
        dissolveController.StartDissolving();
        enabled = false;
    }

    public override void Update()
    {
        base.Update();
    }

    //Simple way to take damage
    public override void TakeDamage(int damage, Color damageType, Transform transform)
    {
        if (damageType == Color.green)
        {
            numberPool.NumberSpawn(numbersTransform, 0, damageType, gameObject, false);
        }

        else
        {
            numberPool.NumberSpawn(numbersTransform, damage, damageType, gameObject, false);

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
        TakeDamage(0, Color.green, transform);
    }

}
