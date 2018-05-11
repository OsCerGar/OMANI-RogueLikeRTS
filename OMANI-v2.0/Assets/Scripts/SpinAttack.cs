using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpinAttack : MonoBehaviour {
    [SerializeField] string TagToAttack;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            EnemyNPC.Life -= transform.parent.GetComponent<NPC>().Damage * 2;
            if (EnemyNavMesh != null)
            {
                EnemyNavMesh.velocity = (other.transform.position - transform.position) * 10;
            }
        }
    }
    
}
