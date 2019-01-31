using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : NPC
{
    /*
    public delegate void OnDeath();
    public static event OnDeath playerDead;
    */

    Powers powers;
    CharacterMovement characterMovement;
    bool protection;
    void Awake()
    {
        boyType = "Swordsman";
        powers = GetComponent<Powers>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    public override void Die()
    {
        SceneManager.LoadScene("GAME_ALPHA", LoadSceneMode.Single);
    }
    public override void TakeDamage(int damage, Color damageType)
    {
        //He dies if life lowers 
        //TODO : Make this an animation, and make it so that it swaps his layer and tag to something neutral

        if (!protection)
        {
            if (powers.powerPool > 0)
            {
                StartCoroutine(gotHit());

                powers.reduceAsMuchPower(damage);

                if (powers.powerPool < 1)
                {
                    Debug.Log("Close Death");
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
        characterMovement.enabled = false;
        yield return new WaitForSeconds(5f);

        characterMovement.enabled = true;
        powers.enabled = true;
        powers.powerPool = 25;

    }

    IEnumerator DamageProtection()
    {
        protection = true;
        yield return new WaitForSeconds(2f);
        protection = false;
    }
}

