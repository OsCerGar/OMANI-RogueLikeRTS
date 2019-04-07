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

    //Inputs
    PlayerInputInterface inputController;
    void Awake()
    {
        boyType = "Swordsman";
        powers = GetComponent<Powers>();
        characterMovement = GetComponent<CharacterMovement>();
        inputController = FindObjectOfType<PlayerInputInterface>();

    }

    public override void Die()
    {
        Director.Play();
    }
    public override void TakeDamage(int damage, Color damageType)
    {
        //He dies if life lowers 
        //TODO : Make this an animation, and make it so that it swaps his layer and tag to something neutral
        numberPool.NumberSpawn(numbersTransform, damage, Color.red, numbersTransform.gameObject, false);

        if (!protection)
        {
            if (powers.armor > 0)
            {
                StartCoroutine(gotHit());

                powers.reduceAsMuchPower(damage);
                inputController.SetVibration(0, 1f, 0.25f, false);
                inputController.SetVibration(1, 1f, 0.25f, false);

                if (powers.armor < 1)
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
        inputController.SetVibration(0, 1f, 1f, false);
        inputController.SetVibration(1, 1f, 1f, false);
        inputController.SetDS4Lights(new Color(1f, 0f, 0.0f, 1f));

        powers.enabled = false;
        characterMovement.speed = 0;
        yield return new WaitForSeconds(5f);
        if (state != "Dead")
        {
            characterMovement.speed = characterMovement.originalSpeed;
            powers.enabled = true;
            powers.armor = 25;
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

