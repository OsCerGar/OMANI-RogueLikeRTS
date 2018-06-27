using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleAttack : MonoBehaviour {
    [SerializeField] bool Knockback;
    [SerializeField] ParticleSystem Effect;
    [SerializeField] AttackSoundsManager Sounds;
    [SerializeField] NPC thisNpcScript;
    bool missed;
    
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        string tagToAttack,secondTagToAttack;
        if(thisNpcScript.transform.tag == "Enemy")
        {
            tagToAttack = "People";
            secondTagToAttack = "Player";
        } else 
        {
            tagToAttack = "Enemy";
            secondTagToAttack = "Enemy";
        }
        if ( other.tag == tagToAttack || other.tag == secondTagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(thisNpcScript.Damage, true, 5,transform.parent.transform);
            }else
            {
                EnemyNPC.TakeDamage(thisNpcScript.Damage);
            }
            //Set his Enemy to this
            EnemyNPC.AI_SetEnemy(transform.parent.gameObject);
            //If he's dead, then forget about him
            missed = false;
           
        }
    }
    private void OnEnable()
    {
        missed = true; 
        GameObject enem = thisNpcScript.AI_GetEnemy();
        if (Effect != null)
        {

            Debug.Log("EnemyParticle");
            Effect.Play();
        }
        if (enem != null)
        {
            if (enem.tag == "Building")
            {
                enem.GetComponent<NPC>().Life -= transform.root.GetComponent<NPC>().Damage;
            }
        }
        
        StartCoroutine(WaitandDisable());
    }
    
    IEnumerator WaitandDisable()
    {
        
        yield return new WaitForSeconds(0.1f);
        if (Sounds != null)
        {
            if (missed)
            {
                Sounds.AttackMiss();
            }else
            {
                Sounds.AttackHit();
            }
        }
        transform.gameObject.SetActive(false);
    }
}
