using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleAttack : MonoBehaviour {
    [SerializeField] string TagToAttack;
    [SerializeField] float PushBack = 0;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            
            EnemyNPC.Life -= transform.parent.GetComponent<NPC>().Damage;
            if (EnemyNavMesh != null)
            {
                EnemyNavMesh.velocity = (other.transform.position - transform.position).normalized * PushBack;
            }
            if (EnemyNPC.Life <= 0)
            {
                transform.parent.GetComponent<NPC>().AI_SetTarget(null);
            }


        }
    }
    private void OnEnable()
    {
        Debug.Log("funciona");
        StartCoroutine(WaitandDisable());
    }
    IEnumerator WaitandDisable()
    {
        
        yield return new WaitForSeconds(0.1f);
        transform.gameObject.SetActive(false);
    }
}
