using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleAttack : MonoBehaviour {
    [SerializeField] string TagToAttack,secondTagToAttack;
    [SerializeField] float PushBack = 0;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToAttack || other.tag == secondTagToAttack )
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            
            EnemyNPC.Life -= transform.parent.GetComponent<NPC>().Damage;

            EnemyNPC.AI_SetEnemy(transform.parent.gameObject);

            if (EnemyNavMesh != null)
            {
                EnemyNavMesh.velocity = (other.transform.position - transform.position).normalized * PushBack;
            }
            if (EnemyNPC.Life <= 0)
            {
                transform.parent.GetComponent<NPC>().AI_SetEnemy(null);
            }


        }
    }
    private void OnEnable()
    {
        var enem = transform.parent.GetComponent<NPC>().AI_GetEnemy();
        if (enem != null)
        {
            if (enem.tag == "Building")
            {
                Debug.Log("damage to building");
                enem.GetComponent<NPC>().Life -= transform.parent.GetComponent<NPC>().Damage;
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
