using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleAttack : MonoBehaviour {
    [SerializeField] bool Knockback;
    [SerializeField] float ActiveHitboxTime = 0.1f;
    [SerializeField] LayerMask Layer;
    [HideInInspector]public  bool PowerUp;
    [SerializeField] ParticleSystem Effect;
    [SerializeField] NPC thisNpcScript;
    string tagToAttack, secondTagToAttack;
    bool missed;
    ParticleSystem PowerUpEffect, PowerUpHitEffect;
    
    private void Start()
    {
        if (thisNpcScript.transform.tag == "Enemy")
        {
            tagToAttack = "People";
            secondTagToAttack = "Player";
        }
        else
        {
            tagToAttack = "Enemy";
            secondTagToAttack = "Enemy";
        }

    }
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        
        if ( other.tag == tagToAttack || other.tag == secondTagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            var attackDamage = thisNpcScript.Damage;

            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(attackDamage, true, 5,transform.parent.transform);
                
            }
            else
            {
                EnemyNPC.TakeDamage(attackDamage);
            }
            //If he's dead, then forget about him
            missed = false;
           
        }
    }
    private void OnEnable()
    {
        missed = true; 
        if (Effect != null)
        {

            Debug.Log("EnemyParticle");
            Effect.Play();
        }
        
        
        StartCoroutine(WaitandDisable());
    }
    
    IEnumerator WaitandDisable()
    {
        
        yield return new WaitForSeconds(ActiveHitboxTime);
        
        transform.gameObject.SetActive(false);
    }
    public void ActivateBoostAttack()
    { /*
        PowerUpEffect.Play();
        PowerUp = true;
        */
    }
    public void DeactivateBoostAttack()
    {
        /*
        PowerUpEffect.Stop();
        */
    }
    public void PowerUpHit()
    {
        /*
        DeactivateBoostAttack();
        PowerUpHitEffect.Play();
        */
    }
}
