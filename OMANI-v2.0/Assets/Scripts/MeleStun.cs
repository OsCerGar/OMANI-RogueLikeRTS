using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class MeleStun : MonoBehaviour {

    [SerializeField] string TagToAttack;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            EnemyNPC.Life -= transform.parent.GetComponent<NPC>().Damage;
            EnemyNavMesh.velocity = (other.transform.position - transform.position).normalized * 10;

            var goStunned = (SharedBool)other.gameObject.GetComponent<BehaviorTree>().GetVariable("Stunned");
            goStunned.Value = true;
            var time = (SharedFloat)other.gameObject.GetComponent<BehaviorTree>().GetVariable("Stunned");
            time.Value = 3f;
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
