using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public class Player : NPC
{
    /*
    public delegate void OnDeath();
    public static event OnDeath playerDead;
    */

    Powers powers;
    CharacterMovement characterMovement;
    [SerializeField] PlayableDirector Director;
    bool protection;
    void Awake()
    {
        boyType = "Swordsman";
        powers = GetComponent<Powers>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    public override void Die()
    {
        Director.Play();
    }
    public override void TakeDamage(int damage, Color damageType)
    {
        //He dies if life lowers 
        //TODO : Make this an animation, and make it so that it swaps his layer and tag to something neutral
        numberPool.NumberSpawn(numbersTransform, damage, Color.red, numbersTransform.gameObject);

        if (!protection)
        {
            if (powers.powerPool > 0)
            {
                StartCoroutine(gotHit());

                powers.reduceAsMuchPower(damage);

                if (powers.powerPool < 1)
                {
                    StartCoroutine(DamageProtection());
                    StartCoroutine(CoolDown());
                }

            }

            else
            {
                //provisional :D
                Die();
                state = "Dead";
            }

        }

    }

    IEnumerator CoolDown()
    {

        powers.enabled = false;
        characterMovement.speed = 0;
        yield return new WaitForSeconds(5f);
        if (state != "Dead")
        {
            characterMovement.speed = characterMovement.originalSpeed;
            powers.enabled = true;
            powers.powerPool = 25;
        }

    }

    IEnumerator DamageProtection()
    {

        protection = true;
        yield return new WaitForSeconds(1f);
        if (state != "Dead")
        {
            protection = false;
        }
    }
}

