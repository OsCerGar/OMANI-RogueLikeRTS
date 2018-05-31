using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class RangedAttack : MonoBehaviour {

    [SerializeField] string TagToAttack;
    [SerializeField] float PushBack;
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
            EnemyNPC.TakeDamage(transform.parent.GetComponent<NPC>().Damage, true, 5);
            //Set his Enemy to this
            EnemyNPC.AI_SetEnemy(transform.parent.gameObject);
            //If he's dead, then forget about him

        }
    }
}
