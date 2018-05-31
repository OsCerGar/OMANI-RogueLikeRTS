using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class RangedAttack : MonoBehaviour {
    
    [SerializeField] bool Knockback;
    [SerializeField] int Damage = 1;




    void OnParticleCollision(GameObject other)
    {
        string tagToAttack, secondTagToAttack;
        if (transform.parent.tag == "Enemy")
        {
            tagToAttack = "People";
            secondTagToAttack = "Player";
        }
        else
        {
            tagToAttack = "Enemy";
            secondTagToAttack = "Enemy";
        }
        if (other.tag == tagToAttack || other.tag == secondTagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(transform.parent.GetComponent<NPC>().Damage, true, 5, transform.parent.transform);
            }else
            {
                EnemyNPC.TakeDamage(transform.parent.GetComponent<NPC>().Damage);
            }
            //Set his Enemy to this
            EnemyNPC.AI_SetEnemy(transform.parent.gameObject);
            //If he's dead, then forget about him

        }
    }
}
