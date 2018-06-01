using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleAttack : MonoBehaviour {
    [SerializeField] bool Knockback;
    [SerializeField] ParticleSystem Effect;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        string tagToAttack,secondTagToAttack;
        if(transform.parent.tag == "Enemy")
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
                EnemyNPC.TakeDamage(transform.parent.GetComponent<NPC>().Damage, true, 5,transform.parent.transform);
            }else
            {
                EnemyNPC.TakeDamage(transform.parent.GetComponent<NPC>().Damage);
            }
            //Set his Enemy to this
            EnemyNPC.AI_SetEnemy(transform.parent.gameObject);
            //If he's dead, then forget about him
           
        }
    }
    private void OnEnable()
    {
        var enem = transform.parent.GetComponent<NPC>().AI_GetEnemy();
        if (Effect != null)
        {
            Effect.Play();
        }
        if (enem != null)
        {
            if (enem.tag == "Building")
            {
                Debug.Log("damage to building");
                enem.GetComponent<NPC>().Life -= transform.root.GetComponent<NPC>().Damage;
            }
        }
        
        StartCoroutine(WaitandDisable());
    }
    IEnumerator WaitandDisable()
    {
        
        yield return new WaitForSeconds(0.1f);
        transform.gameObject.SetActive(false);
    }
}
